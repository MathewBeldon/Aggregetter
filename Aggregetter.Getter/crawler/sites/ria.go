package sites

import (
	"fmt"
	"strconv"
	"time"

	"aggregetter.getter/crawler"
	"github.com/gocolly/colly/v2"
)

type Ria struct {
	Storage crawler.Storage
	LastUrl string
}

func (ria Ria) GetArticle(abort chan struct{}, urls []string) {

	c := colly.NewCollector()

	if err := c.SetStorage(&ria.Storage); err != nil {
		panic(err)
	}

	c.Limit(&colly.LimitRule{
		DomainGlob:  "*ria.*",
		Delay:       100 * time.Millisecond,
		Parallelism: 2,
	})

	c.OnScraped(func(r *colly.Response) {
		ria.Storage.SavePage(crawler.RequestHash(r.Request.URL.String(), r.Request.Body), r.Request.URL, r.Body)
	})

	c.OnRequest(func(r *colly.Request) {
		fmt.Println("Getting article from: ", r.URL)
	})

	for _, url := range urls {
		select {
		case <-abort:
			return
		default:
			c.Visit(url)
		}
	}

}

func (ria Ria) GetLinks(abort chan struct{}) <-chan []string {
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
	channel := ria.GetLinks(abort)
out:
	for links := range channel {
		for _, link := range links {
			if link == ria.LastUrl {
				fmt.Println("SAME FOUND" + link)
				close(abort)
				break out
			}
		}
		ria.GetArticle(abort, links)
	}
}
