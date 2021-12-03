using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleList
{
    public class GetArticlePagedListQuery : IRequest<IEnumerable<ArticlePagedListVm>>
    {
        public int page { get; set; }
    }
}
