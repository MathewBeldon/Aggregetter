package main

import (
	"github.com/MathewBeldon/Aggregetter.Getter/crawler"
	"github.com/MathewBeldon/Aggregetter.Getter/crawler/sites"
)

func main() {
	var client crawler.Client
	tassClient := &sites.Tass{
		Url: "https://tass.ru/rss/v2.xml",
	}
	client = tassClient
	client.GetBody()
}
