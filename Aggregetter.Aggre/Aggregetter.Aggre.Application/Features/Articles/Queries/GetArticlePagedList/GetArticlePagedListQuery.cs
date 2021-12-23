using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public sealed class GetArticlePagedListQuery : IRequest<GetArticlePagedListQueryResponse>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
