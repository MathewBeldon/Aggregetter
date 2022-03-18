using Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults;
using Aggregetter.Aggre.Application.Models.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<GetArticleSearchResultsQueryResponse>> SearchAsync([FromQuery] PaginationRequest paginationRequest, string searchString)
        {
            var getSearchResultsResponse = await _mediator.Send(new GetArticleSearchResultsQuery
            {
                Page = paginationRequest.Page,
                PageSize = paginationRequest.PageSize,
                SearchString = searchString
            });

            return Ok(getSearchResultsResponse);
        }
    }
}
