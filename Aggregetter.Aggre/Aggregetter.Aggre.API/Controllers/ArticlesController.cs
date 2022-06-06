using Aggregetter.Aggre.API.Attributes;
using Aggregetter.Aggre.API.Controllers.Helpers;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Commands.TranslateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProviderAndCategory;
using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public sealed class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PagedAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var articlePagedResponse = await _mediator.Send(new GetArticlesQuery
            {
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return articlePagedResponse.ReturnResult();
        }

        [HttpGet("category/{categoryId:int}"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PagedByCategoryAsync([FromQuery] PaginationRequest paginationRequest, int categoryId)
        {
            var articleByCategoryPagedResponse = await _mediator.Send(new GetArticlesByCategoryQuery
            {
                CategoryId = categoryId,
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return articleByCategoryPagedResponse.ReturnResult();            
        }

        [HttpGet("provider/{providerId:int}"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PagedByProviderAsync([FromQuery] PaginationRequest paginationRequest, int providerId)
        {
            var articleByProviderPagedResponse = await _mediator.Send(new GetArticlesByProviderQuery
            {
                ProviderId = providerId,
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return articleByProviderPagedResponse.ReturnResult();
        }

        [HttpGet("provider/{providerId:int}/category/{categoryId:int}")]
        public async Task<IActionResult> PagedByProviderAndCategoryAsync([FromQuery] PaginationRequest paginationRequest, int categoryId, int providerId)
        {           
            var articleByCategoryPagedResponse = await _mediator.Send(new GetArticlesByProviderAndCategoryQuery
            {
                ProviderId = providerId,
                CategoryId = categoryId,
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return articleByCategoryPagedResponse.ReturnResult();
        }

        [HttpGet("{articleSlug}")]
        public async Task<IActionResult> DetailsAsync(string articleSlug)
        {
            var getArticleDetailsQueryResponse = await _mediator.Send(new GetArticleDetailsQuery()
            {
                ArticleSlug = articleSlug
            });
            return getArticleDetailsQueryResponse.ReturnResult();
        }

        [HttpPost]
        //[Authorise(Role.Basic, Role.Editor)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateArticleCommand createArticleCommand)
        {
            return (await _mediator.Send(createArticleCommand)).ReturnResult();
        }

        [HttpPost("translate")]
        [Authorise(Role.Basic, Role.Editor)]
        public async Task<IActionResult> TranslateAsync([FromBody] TranslateArticleCommand translateArticleCommand)
        {
            return (await _mediator.Send(translateArticleCommand)).ReturnResult();
        }
    }
}
