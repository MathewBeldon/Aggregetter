namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleDto
    {
        public int ArticleId { get; set; }
        public int ProviderId { get; set; }
        public int CategoryId { get; set; }
        public string TranslatedTitle { get; set; }
        public string OriginalTitle { get; set; }
        public string TranslatedBody { get; set; }
        public string OriginalBody { get; set; }
        public string Endpoint { get; set; }
        public string ArticleSlug { get; set; }
    }
}
