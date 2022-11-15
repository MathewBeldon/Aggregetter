using Aggregetter.Aggre.Application.Pipelines.Caching;
using Mediator;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsQuery : IRequest<GetArticleDetailsQueryResponse>, ICacheableRequest
    {
        public string ArticleSlug { get; set; }
        public string Key => $"{ArticleSlug}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
    }
}
