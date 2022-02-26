using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories
{
    public sealed class GetCategoriesQuery : IRequest<GetProvidersQueryREsponse>, ICacheableRequest
    {
        public string Key => $"{nameof(GetCategoriesQuery)}";
        public bool Bypass { get; init; }
        public TimeSpan? AbsoluteExpiration { get; init; }
    }
}
