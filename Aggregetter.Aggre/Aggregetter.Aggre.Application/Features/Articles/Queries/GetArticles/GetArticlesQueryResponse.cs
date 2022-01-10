using Aggregetter.Aggre.Application.Responses;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed class GetArticlesQueryResponse : PagedResponse<List<GetArticlesDto>>
    {
        public GetArticlesQueryResponse(List<GetArticlesDto> data = default(List<GetArticlesDto>)) : base(data)
        {
            if (!(data?.Count > 0)) Message = "Page contains no data"; 
        }
    }
}
