namespace Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories
{
    public sealed record GetCategoriesDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
}