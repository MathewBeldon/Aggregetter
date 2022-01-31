using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider;
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
    public sealed class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetArticlesQueryResponse>> PagedAsync([FromQuery] PaginationRequest paginationRequest, int? categoryId, int? providerId)
        {
            if (categoryId is not null)
            {
                var articleByCategoryPagedResponse = await _mediator.Send(new GetArticlesByCategoryQuery
                {
                    CategoryId = categoryId.Value,
                    Page = paginationRequest.Page,
                    PageSize = paginationRequest.PageSize
                });
                return Ok(articleByCategoryPagedResponse);
            }

            if (providerId is not null)
            {
                var articleByProviderPagedResponse = await _mediator.Send(new GetArticlesByProviderQuery
                {
                    ProviderId = providerId.Value,
                    Page = paginationRequest.Page,
                    PageSize = paginationRequest.PageSize
                });
                return Ok(articleByProviderPagedResponse);
            }

            var articlePagedResponse = await _mediator.Send(new GetArticlesQuery
            {
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize
            });
            return Ok(articlePagedResponse);
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
