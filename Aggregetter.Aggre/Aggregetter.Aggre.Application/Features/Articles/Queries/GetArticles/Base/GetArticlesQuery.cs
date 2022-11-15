using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using Mediator;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQuery : IRequest<GetArticlesQueryResponse>, ICacheableRequest, IPaginationRequest
    {
        public string Key => $"Article-{Page}-{PageSize}";
        public bool Bypass { get; init; }
        public TimeSpan? AbsoluteExpiration { get; init; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
