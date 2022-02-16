namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed record CreateArticleDto(
        int Id,
        int ProviderId,
        int CategoryId,
        string TranslatedTitle,
        string OriginalTitle,
        string TranslatedBody,
        string OriginalBody,
        string Endpoint,
        string ArticleSlug
    );
}
