using Aggregetter.Aggre.Application.Models.Authentication;
using Aggregetter.Aggre.Application.Models.Base;

namespace Aggregetter.Aggre.Application.Features.Accounts.Commands.CreateAccount
{
    public sealed class CreateAccountCommandResponse : ContentResponse<RegistrationResponse>
    {
        public CreateAccountCommandResponse() : base() { }
        public CreateAccountCommandResponse(RegistrationResponse data) : base(data) { }
    }
}