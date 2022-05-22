package crawler

type Article struct {
	Title string
	Date  string
	Body  []string
}

type Client interface {
	GetArticle(articleUrl string) Article
	GetLinks() map[string]string
}
