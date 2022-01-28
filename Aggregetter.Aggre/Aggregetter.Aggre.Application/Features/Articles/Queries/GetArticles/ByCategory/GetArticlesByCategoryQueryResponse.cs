using Aggregetter.Aggre.Application.Models.Pagination;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory
{
    public class GetArticlesByCategoryQueryResponse : PaginationResponse<List<GetArticlesDto>>
    {
        public GetArticlesByCategoryQueryResponse(List<GetArticlesDto> data = default(List<GetArticlesDto>)) : base(data) { }
    }
}