package main

import (
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
			LastUrl: "https://ria.ru/20220527/ukraina-1791330260.html",
		}
		client = riaClient
		client.FetchArticles()
		defer wg.Done()
	}()

	wg.Wait()
}
