using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using Mediator;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider
{
    public sealed class GetArticlesByProviderQuery : IRequest<GetArticlesQueryResponse>, ICacheableRequest, IPaginationRequest
    {
        public int ProviderId { get; set; }
        public string Key => $"Article-{Page}-{PageSize}-Provider-{ProviderId}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
