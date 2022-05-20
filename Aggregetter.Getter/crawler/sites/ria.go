package sites

import (
	"fmt"
	"io"
	"log"
	"net/http"
	"strconv"
	"time"

	"github.com/gocolly/colly/v2"
)

type Ria struct{}

type link struct {
	Link     string `selector:"div.list-item__content > a.list-item__title.color-font-hover-only" attr:"href"`
	DateTime string `selector:"div.list-item__info > div.list-item__date"`
}

func (ria Ria) GetArticle(articleUrl string) string {

	client := &http.Client{}
	req, err := http.NewRequest("GET", articleUrl, nil)
	if err != nil {
		fmt.Println("error")
	}

	req.Header.Add("User-Agent", `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:100.0) Gecko/20100101 Firefox/100.0`)
	req.Header.Add("Accept", `text/html`)
	req.Header.Add("Accept-Language", `ru`)
	req.Header.Add("Connection", `keep-alive`)

	resp, err := client.Do(req)

	if err != nil {
		log.Fatalln(err)
	}

	defer resp.Body.Close()
	fmt.Println("Response status:", resp.Status)

	body, err := io.ReadAll(resp.Body)
	if err != nil {
		log.Fatalln(err)
	}

	return string(body)
}

func (ria Ria) GetLinks() map[string]string {

	c := colly.NewCollector()
	links := make(map[string]string)

	c.OnHTML("body > div.list-items-loaded > div", func(e *colly.HTMLElement) {
		link := &link{}
		e.Unmarshal(link)
		if _, found := links[link.Link]; !found {
			links[link.Link] = link.DateTime
		}
	})

	c.Limit(&colly.LimitRule{
		DomainGlob:  "*ria.*",
		Parallelism: 0,
		Delay:       1 * time.Second,
	})

	c.OnRequest(func(r *colly.Request) {
		fmt.Println("Visiting", r.URL)
	})

	for i := 0; i < 1; i++ {
		c.Visit("https://ria.ru/services/search/getmore/?query=&offset=" + strconv.Itoa(i*20))
	}

	for link := range links {
		fmt.Println(link + " " + links[link])
	}

	return links
}
