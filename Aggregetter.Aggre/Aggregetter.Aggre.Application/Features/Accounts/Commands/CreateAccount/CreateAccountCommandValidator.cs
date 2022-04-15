using FluentValidation;

namespace Aggregetter.Aggre.Application.Features.Accounts.Commands.CreateAccount
{
    public sealed class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(command => command.Username)
                .NotEmpty()
                .WithMessage("Enter a username")
                .DependentRules(() =>
                {
                    RuleFor(command => command.Username.Length)
                        .GreaterThanOrEqualTo(3)
                        .OverridePropertyName(nameof(CreateAccountCommand.Username))
                        .WithMessage("Username must be 3 characters or more");
                });

            RuleFor(command => command.Email)
                .NotEmpty()
                .WithMessage("Enter an email address")
                .DependentRules(() =>
                {
                    RuleFor(command => command.Email)
                        .EmailAddress()
                        .WithMessage("Email address must be valid");
                });

            RuleFor(command => command.Password)
                .NotEmpty()
                .WithMessage("Enter a password")
                .DependentRules(() =>
                {
                    RuleFor(command => command.Password.Length)
                        .GreaterThanOrEqualTo(10)
                        .OverridePropertyName(nameof(CreateAccountCommand.Password))
                        .WithMessage("Password must 10 characters or more");
                });
        }
    }
}
