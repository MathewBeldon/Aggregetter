using Aggregetter.Aggre.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Aggregetter.Aggre.API.Services
{
    public sealed class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public string UserId { get; }
    }
}
