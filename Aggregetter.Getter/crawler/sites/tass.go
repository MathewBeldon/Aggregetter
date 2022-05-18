package sites

import (
	"bufio"
	"fmt"
	"net/http"
)

type Tass struct {
	Url string
}

func (tass Tass) GetBody() string {

	client := &http.Client{}
	req, err := http.NewRequest("GET", "https://tass.ru/rss/v2.xml", nil)
	if err != nil {
		fmt.Println("error")
	}

	req.Header.Add("User-Agent", `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:100.0) Gecko/20100101 Firefox/100.0`)
	req.Header.Add("Cookie", `__js_p_=453,1800,0,0; __jhash_=534; __jua_=crawltass; __hash_=; __lhash_=; newsListCounter=1`)
	req.Header.Add("Accept", `text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8`)
	req.Header.Add("Accept-Language", `en-GB,en;q=0.5`)
	req.Header.Add("Accept-Encoding", `gzip, deflate, br`)
	req.Header.Add("Connection", `keep-alive`)

	resp, err := client.Do(req)

	defer resp.Body.Close()
	fmt.Println("Response status:", resp.Status)

	scanner := bufio.NewScanner(resp.Body)
	for scanner.Scan() {
		fmt.Println(scanner.Text())
	}

	if err := scanner.Err(); err != nil {
		panic(err)
	}

	return tass.Url
}
