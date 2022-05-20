package crawler

type Client interface {
	GetArticle(articleUrl string) string
	GetLinks() map[string]string
}
