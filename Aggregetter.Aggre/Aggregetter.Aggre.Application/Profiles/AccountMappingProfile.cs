using Aggregetter.Aggre.Application.Features.Accounts.Commands.CreateAccount;
using Aggregetter.Aggre.Application.Models.Authentication;
using AutoMapper;

namespace Aggregetter.Aggre.Application.Profiles
{
    public sealed class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            #region CreateAccount
            CreateMap<CreateAccountCommand, RegistrationRequest>();
            #endregion CreateAccount
        }
    }
}
