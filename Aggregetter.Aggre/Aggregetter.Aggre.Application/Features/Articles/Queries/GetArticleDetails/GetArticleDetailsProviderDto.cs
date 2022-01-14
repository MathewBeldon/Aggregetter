namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsProviderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetArticleDetailsLanguageDto Language { get; set; }
    }
}
