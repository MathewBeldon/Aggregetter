using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProviderAndCategory;
using Aggregetter.Aggre.Application.Models.Pagination;
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
        public async Task<ActionResult<GetArticlesQueryResponse>> PagedAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var articlePagedResponse = await _mediator.Send(new GetArticlesQuery
            {
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return Ok(articlePagedResponse);
        }

        [HttpGet("category/{categoryId:int}"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetArticlesQueryResponse>> PagedByCategoryAsync([FromQuery] PaginationRequest paginationRequest, int categoryId)
        {
            var articleByCategoryPagedResponse = await _mediator.Send(new GetArticlesByCategoryQuery
            {
                CategoryId = categoryId,
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return Ok(articleByCategoryPagedResponse);            
        }

        [HttpGet("provider/{providerId:int}"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetArticlesQueryResponse>> PagedByProviderAsync([FromQuery] PaginationRequest paginationRequest, int providerId)
        {
            var articleByProviderPagedResponse = await _mediator.Send(new GetArticlesByProviderQuery
            {
                ProviderId = providerId,
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return Ok(articleByProviderPagedResponse);
        }

        [HttpGet("provider/{providerId:int}/category/{categoryId:int}"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetArticlesQueryResponse>> PagedByProviderAndCategoryAsync([FromQuery] PaginationRequest paginationRequest, int categoryId, int providerId)
        {           
            var articleByCategoryPagedResponse = await _mediator.Send(new GetArticlesByProviderAndCategoryQuery
            {
                ProviderId = providerId,
                CategoryId = categoryId,
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return Ok(articleByCategoryPagedResponse);
        }

        [HttpGet("{articleSlug}", Name = "Details"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetArticleDetailsQueryResponse>> DetailsAsync(string articleSlug)
        {
            var getArticleDetailsQueryResponse = await _mediator.Send(new GetArticleDetailsQuery()
            {
                ArticleSlug = articleSlug
            });
            return Ok(getArticleDetailsQueryResponse);
        }

        [HttpPost(Name = "Create")]
        public async Task<ActionResult<CreateArticleCommandResponse>> CreateAsync([FromBody] CreateArticleCommand createArticleCommand)
        {
            return Ok(await _mediator.Send(createArticleCommand));
        }
    }
}
