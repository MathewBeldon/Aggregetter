using Aggregetter.Aggre.Application.Pipelines.Caching;
using Aggregetter.Aggre.Application.Pipelines.Pagination;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory
{
    public sealed class GetArticlesByCategoryQuery : IRequest<GetArticlesByCategoryQueryResponse>, ICacheableRequest, IPaginationRequest
    {
        public int CategoryId { get; set; }
        public string Key => $"Article-{Page}-{PageSize}-Category-{CategoryId}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
        public int Page { get ; init; }
        public int PageSize { get ; init; }
    }
}
