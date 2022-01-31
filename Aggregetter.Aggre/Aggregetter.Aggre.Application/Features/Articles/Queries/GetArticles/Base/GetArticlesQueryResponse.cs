using Aggregetter.Aggre.Application.Models.Pagination;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQueryResponse : PaginationResponse<List<GetArticlesDto>>
    {
        public GetArticlesQueryResponse(List<GetArticlesDto> data, int page,
            int pageSize, int recordCount) : base(data, page, pageSize, recordCount) { }
    }
}
