namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetSearchResults
{
    public sealed record GetSearchResultsDto(
        string ArticleSlug,
        string OriginalTitle,
        string TranslatedTitle,
        string OriginalBodySnippet,
        string TranslatedBodySnippet
    );
}