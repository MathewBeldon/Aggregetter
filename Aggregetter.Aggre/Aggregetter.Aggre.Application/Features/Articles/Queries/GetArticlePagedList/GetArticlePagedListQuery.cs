using Aggregetter.Aggre.Application.Requests;
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
        public PagedRequest PagedRequest { get; set; }
    }
}
