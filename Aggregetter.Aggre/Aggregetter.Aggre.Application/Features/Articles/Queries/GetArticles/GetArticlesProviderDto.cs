using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed record GetArticlesProviderDto(
        string Name,
        Uri BaseAddress
    );
}
