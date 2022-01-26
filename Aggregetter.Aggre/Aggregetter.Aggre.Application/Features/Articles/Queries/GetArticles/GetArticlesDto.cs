namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed record GetArticlesDto
    {
        public string ArticleSlug { get; init; }
        public string TranslatedTitle { get; init; }      
        public string Endpoint { get; init; }
        public GetArticlesCategoryDto Category { get; init; }
        public GetArticlesProviderDto Provider { get; init; }        
    }
}
