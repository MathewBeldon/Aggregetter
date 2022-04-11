using Aggregetter.Aggre.Application.Features.Accounts.Commands.CreateAccount;
using Aggregetter.Aggre.Application.Models.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using System;
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
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            throw new NotImplementedException();
            //return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [FeatureGate("Registration")]
        [HttpPost("register")]
        public async Task<ActionResult<CreateAccountCommandResponse>> RegisterAsync(CreateAccountCommand createAccountCommand)
        {
            var response = await _mediator.Send(createAccountCommand);
            return Ok(response);
        }
    }
}
