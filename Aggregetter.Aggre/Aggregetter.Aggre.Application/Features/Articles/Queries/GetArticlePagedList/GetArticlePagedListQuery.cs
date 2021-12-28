using Aggregetter.Aggre.Application.Features.Pipelines.Caching;
using Aggregetter.Aggre.Application.Requests;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList
{
    public sealed class GetArticlePagedListQuery : IRequest<GetArticlePagedListQueryResponse>, ICacheableQuery
    {
        public PagedRequest PagedRequest { get; set; }
        public string Key => $"Article-{PagedRequest.Page}-{PagedRequest.PageSize}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
