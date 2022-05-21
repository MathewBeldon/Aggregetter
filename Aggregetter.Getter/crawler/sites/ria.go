package sites

import (
	"fmt"
	"strconv"
	"strings"
	"time"

	"aggregetter.getter/crawler"
	"github.com/gocolly/colly/v2"
)

type Ria struct {
	LastUrl string
	Article crawler.Article
}

type link struct {
	Link  string `selector:"div.list-item__content > a.list-item__title.color-font-hover-only" attr:"href"`
	Title string `selector:"div.list-item__content > a.list-item__title.color-font-hover-only"`
}

type article struct {
	Title string `selector:"div > div > div.article__header > div.article__title"`
	Date  string `selector:"div > div > div.article__header > div.article__info > div.article__info-date"`
}

func (ria Ria) GetArticle(articleUrl string) crawler.Article {

	c := colly.NewCollector()
	article := &article{}
	c.OnHTML("div > div > div > div.layout-article__over > div.layout-article__main", func(e *colly.HTMLElement) {
		e.Unmarshal(article)
	})

	c.Limit(&colly.LimitRule{
		DomainGlob:  "*ria.*",
		Parallelism: 2,
		Delay:       3 * time.Second,
	})

	c.OnRequest(func(r *colly.Request) {
		fmt.Println("Visiting", r.URL)
	})

	c.Visit(articleUrl)

	return crawler.Article{
		Title: article.Title,
		Date:  article.Date,
	}
}

func (ria Ria) GetLinks() map[string]string {

	c := colly.NewCollector()
	links := make(map[string]string)
	finished := false
	c.OnHTML("body > div.list-items-loaded > div", func(e *colly.HTMLElement) {
		link := &link{}
		e.Unmarshal(link)
		if link.Link == ria.LastUrl {
			finished = true
		}
		if _, found := links[link.Link]; !found && !finished && strings.Contains(link.Link, "https://ria.ru/") {
			links[link.Link] = link.Title
		}
	})

	c.Limit(&colly.LimitRule{
		DomainGlob:  "*ria.*",
		Parallelism: 0,
		Delay:       7 * time.Second,
	})

	c.OnRequest(func(r *colly.Request) {
		fmt.Println("Visiting", r.URL)
	})

	for i := 0; !finished; i++ {
		c.Visit("https://ria.ru/services/search/getmore/?query=&offset=" + strconv.Itoa(i*20))
	}

	return links
}
