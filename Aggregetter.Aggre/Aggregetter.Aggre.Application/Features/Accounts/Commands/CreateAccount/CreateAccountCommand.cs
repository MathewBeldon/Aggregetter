using MediatR;

namespace Aggregetter.Aggre.Application.Features.Accounts.Commands.CreateAccount
{
    public sealed class CreateAccountCommand : IRequest<CreateAccountCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
