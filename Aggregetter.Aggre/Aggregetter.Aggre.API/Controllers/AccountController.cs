using Aggregetter.Aggre.Application.Contracts.Identity;
using Aggregetter.Aggre.Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAuthenticationService authenticationService, ILogger<AccountController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("authenticate")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                             nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("deauthenticate")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                             nameof(DefaultApiConventions.Get))]
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
        [ApiConventionMethod(typeof(DefaultApiConventions),
                             nameof(DefaultApiConventions.Post))]
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
