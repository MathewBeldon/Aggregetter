using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider
{
    public sealed class GetArticlesByProviderQuery : IRequest<GetArticlesQueryResponse>, ICacheableRequest
    {
        public int ProviderId { get; set; }
        public string Key => $"Article-{Page}-{PageSize}-Provider-{ProviderId}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}
