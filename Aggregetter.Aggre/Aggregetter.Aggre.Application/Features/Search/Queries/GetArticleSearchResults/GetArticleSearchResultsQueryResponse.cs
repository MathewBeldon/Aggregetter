using Aggregetter.Aggre.Application.Models.Base;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults
{
    public sealed class GetArticleSearchResultsQueryResponse : PaginationResponse<List<GetArticleSearchResultsDto>>
    {
        public GetArticleSearchResultsQueryResponse(List<GetArticleSearchResultsDto> data)
            : base(data) { }
    }
}