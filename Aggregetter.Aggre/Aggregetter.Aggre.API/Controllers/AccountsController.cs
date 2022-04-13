using Aggregetter.Aggre.Application.Features.Accounts.Commands.CreateAccount;
using Aggregetter.Aggre.Application.Features.Accounts.Queries.AuthenticateAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public sealed class AccountsController : Controller
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateAccountQueryResponse>> AuthenticateAsync(AuthenticateAccountQuery authenticateAccountQuery)
        {
            return Ok(await _mediator.Send(authenticateAccountQuery));
        }

        [FeatureGate("Registration")]
        [HttpPost("register")]
        public async Task<ActionResult<CreateAccountCommandResponse>> RegisterAsync(CreateAccountCommand createAccountCommand)
        {
            return Ok(await _mediator.Send(createAccountCommand));
        }
    }
}
