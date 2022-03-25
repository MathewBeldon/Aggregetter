using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProviderAndCategory
{
    public sealed class GetArticlesByProviderAndCategoryQuery : PaginationRequest, IRequest<GetArticlesQueryResponse>, ICacheableRequest
    {
        public int ProviderId { get; set; }
        public int CategoryId { get; set; }
        public string Key => $"Article-{Page}-{PageSize}-Provider-{ProviderId}-Category-{CategoryId}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
    }
}
