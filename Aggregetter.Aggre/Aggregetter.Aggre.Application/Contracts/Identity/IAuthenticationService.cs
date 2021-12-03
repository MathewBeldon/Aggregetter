using Aggregetter.Aggre.Application.Models.Authentication;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task DeauthenticateAsync();
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
