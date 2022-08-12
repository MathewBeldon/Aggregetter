using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory
{
    public sealed class GetArticlesByCategoryQuery : IRequest<GetArticlesQueryResponse>, ICacheableRequest, IPaginationRequest
    {
        public int CategoryId { get; set; }
        public string Key => $"Article-{Page}-{PageSize}-Category-{CategoryId}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
