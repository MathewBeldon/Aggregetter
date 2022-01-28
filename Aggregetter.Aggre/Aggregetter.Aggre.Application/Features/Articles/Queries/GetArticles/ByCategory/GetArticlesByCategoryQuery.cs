using Aggregetter.Aggre.Application.Models.Pagination;
using MediatR;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory
{
    public sealed class GetArticlesByCategoryQuery : IRequest<GetArticlesByCategoryQueryResponse>
    {
        public int CategoryId { get; set; }
        public PaginationRequest PaginationRequest { get; set; }
        public string Key => $"Article-{PaginationRequest.Page}-{PaginationRequest.PageSize}-Category-{CategoryId}";
        public bool Bypass { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
    }
}
