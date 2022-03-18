namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults
{
    public sealed record GetArticleSearchResultsDto(
        string ArticleSlug,
        string TranslatedTitle
    );
}