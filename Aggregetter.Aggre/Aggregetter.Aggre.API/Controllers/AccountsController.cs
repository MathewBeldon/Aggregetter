using Aggregetter.Aggre.Application.Contracts.Identity;
using Aggregetter.Aggre.Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AccountsController> _logger;
        public AccountsController(IAuthenticationService authenticationService, ILogger<AccountsController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("deauthenticate")]
        public async Task<ActionResult> DeauthenticateAsync(string returnUrl = null)
        {
            await _authenticationService.DeauthenticateAsync();
            if (returnUrl is not null)
            {
                return LocalRedirect(returnUrl);
            }
            return Ok();
        }

        [FeatureGate("Registration")]
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _authenticationService.RegisterAsync(request));
            }
            return BadRequest(ModelState);
        }
    }
}
