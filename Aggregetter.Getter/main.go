package main

import (
	"github.com/MathewBeldon/Aggregetter.Getter/crawler"
	"github.com/MathewBeldon/Aggregetter.Getter/crawler/sites"
)

func main() {
	var client crawler.Client
	riaClient := &sites.Ria{
		Url: "https://radiosputnik.ria.ru",
	}
	client = riaClient
	//	fmt.Println(client.GetArticle("https://radiosputnik.ria.ru/20220519/ukraina-1789580469.html"))
	client.GetLinks()
}
