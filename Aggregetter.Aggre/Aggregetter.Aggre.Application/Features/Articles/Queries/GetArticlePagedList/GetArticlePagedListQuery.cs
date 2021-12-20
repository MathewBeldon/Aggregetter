using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public class GetArticlePagedListQuery : IRequest<ArticlePagedListResponse>
    {
        public int page { get; set; }
    }
}
