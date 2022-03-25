using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider
{
    public sealed class GetArticlesByProviderQuery : PaginationRequest, IRequest<GetArticlesQueryResponse>, ICacheableRequest
    {
        public int ProviderId { get; set; }
        public string Key => $"Article-{Page}-{PageSize}-Provider-{ProviderId}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
    }
}
