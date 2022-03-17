using Aggregetter.Aggre.Application.Models.Pagination;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetSearchResults
{
    public sealed class GetSearchResultsQueryResponse : PaginationResponse<List<GetSearchResultsDto>>
    {
        public GetSearchResultsQueryResponse() : base() { }
        public GetSearchResultsQueryResponse(
            List<GetSearchResultsDto> data,
            int page,
            int pageSize,
            int recordCount
            ) : base(data, page, pageSize, recordCount) { }
    }
}