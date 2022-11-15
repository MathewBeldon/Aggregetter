using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Commands.TranslateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProviderAndCategory;
using Aggregetter.Aggre.Application.Models.Base;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public sealed class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IMediator mediator, ILogger<ArticlesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetArticlesQueryResponse>> PagedAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var result = await _mediator.Send(new GetArticlesQuery
            {
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return Ok(result);            
        }

        [HttpGet("category/{categoryId:int}"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PagedByCategoryAsync([FromQuery] PaginationRequest paginationRequest, int categoryId)
        {
            var result = await _mediator.Send(new GetArticlesByCategoryQuery
            {
                CategoryId = categoryId,
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return Ok(result);
        }

        [HttpGet("provider/{providerId:int}"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PagedByProviderAsync([FromQuery] PaginationRequest paginationRequest, int providerId)
        {
            var result = await _mediator.Send(new GetArticlesByProviderQuery
            {
                ProviderId = providerId,
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return Ok(result);
        }

        [HttpGet("provider/{providerId:int}/category/{categoryId:int}")]
        public async Task<IActionResult> PagedByProviderAndCategoryAsync([FromQuery] PaginationRequest paginationRequest, int categoryId, int providerId)
        {           
            var result = await _mediator.Send(new GetArticlesByProviderAndCategoryQuery
            {
                ProviderId = providerId,
                CategoryId = categoryId,
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });

            return Ok(result);
        }

        [HttpGet("{articleSlug}")]
        public async Task<IActionResult> DetailsAsync(string articleSlug)
        {
            var result = await _mediator.Send(new GetArticleDetailsQuery()
            {
                ArticleSlug = articleSlug
            });

            return Ok(result);
        }

        [HttpPost]
        //[Authorise(Role.Basic, Role.Editor)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateArticleCommand createArticleCommand)
        {
            var result = await _mediator.Send(createArticleCommand);

            return Created("",result);
        }

        [HttpPost("translate")]
        //[Authorise(Role.Basic, Role.Editor)]
        public async Task<IActionResult> TranslateAsync([FromBody] TranslateArticleCommand translateArticleCommand)
        {
            var result = await _mediator.Send(translateArticleCommand);

            return Ok(result);
        }
    }
}
