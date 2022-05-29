package main

import (
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
				VisitedCollection: "aggregetter_visited",
				CookiesCollection: "aggregetter_cookies",
				PagesCollection:   "aggregetter_pages",
			}
			storage.Init()
			riaClient := &sites.Ria{
				LastUrl: storage.GetLastArticle(),
				Storage: *storage,
			}
			client = riaClient
			client.FetchArticles()
			defer wg.Done()
		}()
		wg.Wait()
		time.Sleep(1 * time.Minute)
	}
}
