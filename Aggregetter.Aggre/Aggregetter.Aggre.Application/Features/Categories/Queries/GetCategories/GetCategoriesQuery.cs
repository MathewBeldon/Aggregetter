using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories
{
    public sealed record GetCategoriesQuery : IRequest<GetCategoriesQueryResponse>, ICacheableQuery
    {
        public string Key => $"{nameof(GetCategoriesQuery)}";

        public bool Bypass { get; init; }

        public TimeSpan? AbsoluteExpiration { get; init; }
    }
}
