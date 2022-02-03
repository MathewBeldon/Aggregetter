using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed record GetArticlesProviderDto
    {
        public string Name { get; init; }
        public string BaseAddress { get; init; }
    }
}
