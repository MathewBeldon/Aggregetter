using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults
{
    public sealed class GetArticleSearchResultsQuery : PaginationRequest, IRequest<GetArticleSearchResultsQueryResponse>, ICacheableRequest
    {
        public string Key => $"SearchResult-{SearchString}-{Page}-{PageSize}";
        public bool Bypass { get; init; }
        public TimeSpan? AbsoluteExpiration { get; init; }
        public string SearchString { get; init; }
    }
}
