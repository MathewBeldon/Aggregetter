namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed record GetArticleDetailsProviderDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public GetArticleDetailsLanguageDto Language { get; init; }
    }
}
