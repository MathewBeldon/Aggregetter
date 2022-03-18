using Aggregetter.Aggre.Application.Models.Pagination;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults
{
    public sealed class GetArticleSearchResultsQueryResponse : PaginationResponse<List<GetArticleSearchResultsDto>>
    {
        public GetArticleSearchResultsQueryResponse() : base() { }
        public GetArticleSearchResultsQueryResponse(
            List<GetArticleSearchResultsDto> data,
            int page,
            int pageSize,
            int recordCount
            ) : base(data, page, pageSize, recordCount) { }
    }
}