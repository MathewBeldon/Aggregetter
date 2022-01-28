using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Caching;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQuery : IRequest<GetArticlesQueryResponse>, ICacheableQuery
    {
        public PaginationRequest PaginationRequest { get; set; }
        public string Key => $"Article-{PaginationRequest.Page}-{PaginationRequest.PageSize}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
    }
}
