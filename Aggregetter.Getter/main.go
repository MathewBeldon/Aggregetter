package main

import (
	"fmt"
	"sync"

	"aggregetter.getter/crawler"
	"aggregetter.getter/crawler/sites"
)

//first 2022 https://ria.ru/20211231/siriya-1766334392.html
func main() {
	var wg sync.WaitGroup

	wg.Add(1)
	go func() {
		var client crawler.Client
		riaClient := &sites.Ria{
			LastUrl: "https://ria.ru/20220522/gazoprovod-1790056019.html",
		}
		client = riaClient
		//	fmt.Println(client.GetArticle("https://radiosputnik.ria.ru/20220519/ukraina-1789580469.html"))
		links := client.GetLinks()

		for link := range links {
			article := client.GetArticle(link)
			fmt.Println(article.Title + " DATE: " + article.Date)
			for _, body := range article.Body {
				fmt.Println(body)
				fmt.Println("")
			}
			fmt.Println("---------------------------------------")
			fmt.Println("")

		}
		defer wg.Done()
	}()

	wg.Wait()
}
