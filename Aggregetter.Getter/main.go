package main

import (
	"aggregetter.getter/crawler"
	"aggregetter.getter/crawler/sites"
)

func main() {
	var client crawler.Client
	riaClient := &sites.Ria{}
	client = riaClient
	//	fmt.Println(client.GetArticle("https://radiosputnik.ria.ru/20220519/ukraina-1789580469.html"))
	client.GetLinks()
}
