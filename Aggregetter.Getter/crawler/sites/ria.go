package sites

import (
	"fmt"
	"strconv"

	"aggregetter.getter/crawler"
	"github.com/gocolly/colly/v2"
)

type Ria struct {
	LastUrl string
	Article crawler.Article
}
type article struct {
	Title string   `selector:"div > div > div.article__header > .article__title"`
	Date  string   `selector:"div > div > div.article__header > div.article__info > div.article__info-date"`
	Body  []string `selector:"div > div > div.article__body.js-mediator-article.mia-analytics > div > .article__text"`
}

func GetArticle(articleUrl string) crawler.Article {

	c := colly.NewCollector()
	article := &article{}
	c.OnHTML("div > div > div > div.layout-article__over > div.layout-article__main", func(e *colly.HTMLElement) {
		e.Unmarshal(article)
	})

	c.Limit(&colly.LimitRule{
		DomainGlob:  "*ria.*",
		Parallelism: 3,
	})

	c.OnRequest(func(r *colly.Request) {
		fmt.Println("Getting article from: ", r.URL)
	})

	c.Visit(articleUrl)

	return crawler.Article{
		Title: article.Title,
		Date:  article.Date,
		Body:  article.Body,
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
		for _, link := range links {
			if ria.LastUrl == link {
				close(abort)
				return
			}
			article := GetArticle(link)
			fmt.Println(article.Title + "// DATE: " + article.Date + "// SIZE: " + strconv.Itoa(len(article.Body)))
			// for _, body := range article.Body {
			// 	fmt.Println(body)
			// 	fmt.Println("")
			// }
			fmt.Println("---------------------------------------")
			fmt.Println("")
		}
	}
}
