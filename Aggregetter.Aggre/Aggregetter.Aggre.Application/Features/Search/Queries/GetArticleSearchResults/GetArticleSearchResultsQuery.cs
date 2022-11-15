using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using Mediator;
using System;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults
{
    public sealed class GetArticleSearchResultsQuery : IRequest<GetArticleSearchResultsQueryResponse>, ICacheableRequest, IPaginationRequest
    {
        public string Key => $"SearchResult-{SearchString}-{Page}-{PageSize}";
        public bool Bypass { get; init; }
        public TimeSpan? AbsoluteExpiration { get; init; }
        public string SearchString { get; init; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
