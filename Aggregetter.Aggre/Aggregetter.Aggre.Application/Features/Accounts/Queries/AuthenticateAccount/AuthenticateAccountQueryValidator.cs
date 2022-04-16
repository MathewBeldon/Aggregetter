using FluentValidation;

namespace Aggregetter.Aggre.Application.Features.Accounts.Queries.AuthenticateAccount
{
    public sealed class AuthenticateAccountQueryValidator : AbstractValidator<AuthenticateAccountQuery>
    {
        public AuthenticateAccountQueryValidator()
        {
            RuleFor(command => command.Email)
                .NotEmpty()
                .WithMessage("Enter an email address");

            RuleFor(command => command.Password)
                .NotEmpty()
                .WithMessage("Enter a password");
        }
    }
}
