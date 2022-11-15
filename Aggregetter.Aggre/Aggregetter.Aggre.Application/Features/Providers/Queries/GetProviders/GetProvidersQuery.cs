using Aggregetter.Aggre.Application.Pipelines.Caching;
using Mediator;
using System;

namespace Aggregetter.Aggre.Application.Features.Providers.Queries.GetProviders
{
    public sealed class GetProvidersQuery : IRequest<GetProvidersQueryResponse>, ICacheableRequest
    {
        public string Key => $"{nameof(GetProvidersQuery)}";
        public bool Bypass { get; init; }
        public TimeSpan? AbsoluteExpiration { get; init; }
    }
}
