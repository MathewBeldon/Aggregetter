using Aggregetter.Aggre.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public class ArticlePagedListResponse : PagedResponse<List<ArticlePagedItemDto>>
    {
        public ArticlePagedListResponse(int pageNumber, int pageSize, List<ArticlePagedItemDto> data) : base(pageNumber, pageSize, data)
        {
        }
    }
}
