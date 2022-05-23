package crawler

type Article struct {
	Title string
	Date  string
	Body  []string
}

type Client interface {
	FetchArticles()
}
