using MediatR;

namespace Aggregetter.Aggre.Application.Features.Accounts.Queries.AuthenticateAccount
{
    public sealed class AuthenticateAccountQuery : IRequest<AuthenticateAccountQueryResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
