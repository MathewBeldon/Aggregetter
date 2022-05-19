package sites

import (
	"fmt"
	"io"
	"log"
	"net/http"

	"golang.org/x/net/html"
)

type Ria struct {
	Url string
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

func (ria Ria) GetLinks() []string {

	client := &http.Client{}
	req, err := http.NewRequest("GET", "https://ria.ru/services/search/getmore/?query=&offset=0", nil)
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

	doc, err := html.Parse(resp.Body)
	if err != nil {
		log.Fatalln(err)
	}
	var f func(*html.Node)
	f = func(n *html.Node) {
		fmt.Println(n.Data)

		if n.Type == html.ElementNode && n.Data == "a" {
			fmt.Println(n)
		}
		for c := n.FirstChild; c != nil; c = c.NextSibling {
			f(c)
		}
	}
	f(doc)

	return []string{"test string", string(body)}
}
