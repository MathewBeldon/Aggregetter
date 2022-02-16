namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed record GetArticleDetailsProviderDto(
        int Id,
        string Name,
        GetArticleDetailsLanguageDto Language
    );
}
