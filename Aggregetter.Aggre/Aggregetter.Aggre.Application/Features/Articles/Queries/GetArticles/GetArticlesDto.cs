namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed record GetArticlesDto(
        string ArticleSlug,
        string TranslatedTitle,
        string Endpoint,
        GetArticlesCategoryDto Category,
        GetArticlesProviderDto Provider
    );
}
