using Aggregetter.Aggre.Application.Models.Pagination;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQueryResponse : PaginationResponse<List<GetArticlesDto>>
    {
        public GetArticlesQueryResponse(List<GetArticlesDto> data = default(List<GetArticlesDto>)) : base(data)
        {
            if (!(data?.Count > 0)) Message = "Page contains no data"; 
        }
    }
}
