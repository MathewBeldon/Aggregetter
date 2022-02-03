namespace Aggregetter.Aggre.Application.Features.Providers.Queries.GetProviders
{
    public sealed record GetProvidersDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
}
