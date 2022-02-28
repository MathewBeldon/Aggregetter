using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed record GetArticlesProviderDto(
        int id,
        string Name,
        Uri BaseAddress
    );
}
