using Aggregetter.Aggre.Application.Features.Pipelines.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public class GetArticleDetailsQuery : IRequest<ArticleDetailsVm>, ICacheableQuery
    {
        public string ArticleSlug { get; set; }
        public string Key => $"{ArticleSlug}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
