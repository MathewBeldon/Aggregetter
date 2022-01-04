using Aggregetter.Aggre.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public sealed class GetArticlePagedListQueryResponse : PagedResponse<List<GetArticlePagedItemDto>>
    {
        public GetArticlePagedListQueryResponse(List<GetArticlePagedItemDto> data = default(List<GetArticlePagedItemDto>)) : base(data)
        {
            if (!(data?.Count > 0)) Message = "Page contains no data"; 
        }
    }
}
