using Aggregetter.Aggre.Application.Models.Pagination;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQueryResponse : PaginationResponse<List<GetArticlesDto>>
    {
        public GetArticlesQueryResponse()
            : base() { }
        public GetArticlesQueryResponse(List<GetArticlesDto> data) 
            : base(data) { }
    }
}