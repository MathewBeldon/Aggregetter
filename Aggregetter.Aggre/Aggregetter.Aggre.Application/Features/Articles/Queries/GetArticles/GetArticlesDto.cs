namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed class GetArticlesDto
    {
        public string ArticleSlug { get; set; }
        public string TranslatedTitle { get; set; }
        public GetArticlesCategoryDto Category { get; set; }
        public GetArticlesProviderDto Provider { get; set; }        
    }
}
