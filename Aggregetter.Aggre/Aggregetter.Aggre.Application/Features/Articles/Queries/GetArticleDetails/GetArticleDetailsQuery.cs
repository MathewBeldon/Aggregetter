using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public class GetArticleDetailsQuery : IRequest<ArticleDetailsVm>
    {
        public Guid ArticleId { get; set; }
    }
}
