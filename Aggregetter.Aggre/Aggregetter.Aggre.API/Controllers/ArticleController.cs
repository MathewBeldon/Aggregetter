using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles;
using Aggregetter.Aggre.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetArticlesQueryResponse>> PagedAsync([FromQuery] PagedRequest pagedRequest)
        {
            var articleListPagedResponse = await _mediator.Send(new GetArticlesQuery
            {
                PagedRequest = pagedRequest,
            });
            return Ok(articleListPagedResponse);
        }

        [HttpGet("{articleSlug}", Name = "Details"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ArticleDetailsVm>> DetailsAsync(string articleSlug)
        {
            var articleDetailsVm = await _mediator.Send(new GetArticleDetailsQuery()
            {
                ArticleSlug = articleSlug
            });
            return Ok(articleDetailsVm);
        }

        [ HttpPost(Name = "Create")]
        public async Task<ActionResult<CreateArticleCommandResponse>> CreateAsync([FromBody] CreateArticleCommand createArticleCommand)
        {
            return Ok(await _mediator.Send(createArticleCommand));
        }
    }
}
