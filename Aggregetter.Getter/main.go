package main

import (
	"fmt"
	"sync"

	"aggregetter.getter/crawler"
	"aggregetter.getter/crawler/sites"
)

func main() {
	var wg sync.WaitGroup

	wg.Add(1)
	go func() {
		var client crawler.Client
		riaClient := &sites.Ria{
			LastUrl: "https://ria.ru/20220521/ldpr-1789965521.html",
		}
		client = riaClient
		//	fmt.Println(client.GetArticle("https://radiosputnik.ria.ru/20220519/ukraina-1789580469.html"))
		links := client.GetLinks()

		for link := range links {
			article := client.GetArticle(link)
			fmt.Println(article.Title + "DATE:" + article.Date)
		}
		defer wg.Done()
	}()

	wg.Wait()
}
