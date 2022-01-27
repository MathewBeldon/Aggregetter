using Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories;
using Aggregetter.Aggre.Application.Models.Pagination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public sealed class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(Name = "All"), ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetCategoriesQueryResponse>> AllAsync()
        {
            var categoriesResponse = await _mediator.Send(new GetCategoriesQuery() {  AbsoluteExpiration = TimeSpan.FromSeconds(30) });
            return Ok(categoriesResponse);
        }
    }
}
