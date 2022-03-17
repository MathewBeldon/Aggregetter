using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetSearchResults
{
    public sealed class GetSearchResultsQuery : IRequest<GetSearchResultsQueryResponse>, ICacheableRequest
    {
        public string Key => $"SearchResult-{SearchString}-{Page}-{PageSize}";
        public bool Bypass { get; init; }
        public TimeSpan? AbsoluteExpiration { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
        public string SearchString { get; init; }
    }
}
