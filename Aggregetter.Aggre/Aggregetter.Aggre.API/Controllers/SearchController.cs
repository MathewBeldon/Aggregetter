using Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults;
using Aggregetter.Aggre.Application.Models.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public sealed class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{searchString}")]
        public Task<ActionResult<GetArticleSearchResultsQueryResponse>> SearchAsync([FromQuery] IPaginationRequest paginationRequest, string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
