using Aggregetter.Aggre.Application.Models.Authentication;
using Aggregetter.Aggre.Application.Models.Base;

namespace Aggregetter.Aggre.Application.Features.Accounts.Queries.AuthenticateAccount
{
    public sealed class AuthenticateAccountQueryResponse : ContentResponse<AuthenticationResponse>
    {
        public AuthenticateAccountQueryResponse() : base() { }
        public AuthenticateAccountQueryResponse(AuthenticationResponse data) : base(data) { }
    }
}