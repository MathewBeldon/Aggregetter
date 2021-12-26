using Aggregetter.Aggre.API.Attributes;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList;
using Aggregetter.Aggre.Application.Requests;
using Aggregetter.Aggre.Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<GetArticlePagedListQueryResponse>> PagedAsync([FromQuery] PagedRequest pagedRequest)
        {
            var articleListPagedResponse = await _mediator.Send(new GetArticlePagedListQuery
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

        [Authorise(Role.Editor), HttpPost(Name = "Create")]
        public async Task<ActionResult<CreateArticleCommandResponse>> CreateAsync([FromBody] CreateArticleCommand createArticleCommand)
        {
            return Ok(await _mediator.Send(createArticleCommand));
        }
    }
}
