namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed record CreateArticleDto
    {
        public int Id { get; init; }
        public int ProviderId { get; init; }
        public int CategoryId { get; init; }
        public string TranslatedTitle { get; init; }
        public string OriginalTitle { get; init; }
        public string TranslatedBody { get; init; }
        public string OriginalBody { get; init; }
        public string Endpoint { get; init; }
        public string ArticleSlug { get; init; }
    }
}
