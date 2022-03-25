using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQuery : PaginationRequest, IRequest<GetArticlesQueryResponse>, ICacheableRequest
    {
        public string Key => $"Article-{Page}-{PageSize}";
        public bool Bypass { get; init; }
        public TimeSpan? AbsoluteExpiration { get; init; }
    }
}
