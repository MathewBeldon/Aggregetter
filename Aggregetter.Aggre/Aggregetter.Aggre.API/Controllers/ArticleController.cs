﻿using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles;
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
        public async Task<ActionResult<GetArticlesQueryResponse>> PagedAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var articleListPagedResponse = await _mediator.Send(new GetArticlesQuery
            {
                PaginationRequest = paginationRequest,
            });
            return Ok(articleListPagedResponse);
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
