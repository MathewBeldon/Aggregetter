package main

import (
	"fmt"
	"sync"
	"time"

	"aggregetter.getter/crawler"
	"aggregetter.getter/crawler/sites"
)

//first 2022 https://ria.ru/20211231/siriya-1766334392.html
func main() {
	var wg sync.WaitGroup
	for {
		wg.Add(1)
		go func() {
			var client crawler.Client

			storage := &crawler.Storage{
				Database:          "aggregetter",
				URI:               "mongodb://127.0.0.1:27017",
				VisitedCollection: "aggregetter_ria_visited",
				CookiesCollection: "aggregetter_ria_cookies",
				PagesCollection:   "aggregetter_ria_pages",
			}
			storage.Init()
			lastUrl := storage.GetLastArticle()
			if lastUrl == "" {
				fmt.Println("GETTING VERY OLD")
				lastUrl = "https://ria.ru/20211231/siriya-1766334392.html"
			}
			riaClient := &sites.Ria{
				Storage: *storage,
				LastUrl: lastUrl,
			}
			client = riaClient
			client.FetchArticles()
			defer wg.Done()
		}()
		wg.Wait()
		fmt.Println("waiting one minute for more articles")
		time.Sleep(1 * time.Minute)
	}
}
