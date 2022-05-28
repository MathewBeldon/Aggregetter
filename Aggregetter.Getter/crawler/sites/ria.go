package sites

import (
	"fmt"
	"strconv"
	"time"

	"github.com/gocolly/colly/v2"
	"github.com/zolamk/colly-mongo-storage/colly/mongo"
)

type Ria struct {
	LastUrl string
}

func GetArticle(abort chan struct{}, urls []string, lastUrl string) {

	c := colly.NewCollector()

	c.Limit(&colly.LimitRule{
		DomainGlob:  "*ria.*",
		Delay:       1 * time.Second,
		Parallelism: 1,
	})

	storage := &mongo.Storage{
		Database: "colly",
		URI:      "mongodb://127.0.0.1:27017",
	}

	if err := c.SetStorage(storage); err != nil {
		panic(err)
	}

	c.OnRequest(func(r *colly.Request) {
		fmt.Println("Getting article from: ", r.URL)
	})

	for _, url := range urls {
		if url == lastUrl {
			close(abort)
			return
		}
		c.Visit(url)
	}
}

func GetLinks(abort <-chan struct{}) <-chan []string {
	channel := make(chan []string)
	go func() {
		defer close(channel)
		c := colly.NewCollector()
		c.OnHTML("body > div.list-items-loaded > div", func(e *colly.HTMLElement) {
			channel <- e.ChildAttrs("div.list-item__content > a.list-item__title.color-font-hover-only", "href")
		})

		c.OnRequest(func(r *colly.Request) {
			fmt.Println("Getting links from: ", r.URL)
		})

		c.Limit(&colly.LimitRule{
			DomainGlob:  "*ria.*",
			Delay:       7 * time.Second,
			Parallelism: 1,
		})

		for i := 0; i < 10000; i++ {
			select {
			case <-abort:
				fmt.Println("Aborting")
				return
			default:
				c.Visit("https://ria.ru/services/search/getmore/?query=&offset=" + strconv.Itoa(i*20))
			}
		}
	}()
	return channel
}

func (ria Ria) FetchArticles() {
	abort := make(chan struct{})
	channel := GetLinks(abort)
	for links := range channel {
		GetArticle(abort, links, ria.LastUrl)
	}
}
