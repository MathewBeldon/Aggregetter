using Aggregetter.Aggre.Application.Pipelines.Caching;
using Aggregetter.Aggre.Application.Pipelines.Pagination;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQuery : IRequest<GetArticlesQueryResponse>, ICacheableRequest, IPaginationRequest
    {
        public string Key => $"Article-{Page}-{PageSize}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}
