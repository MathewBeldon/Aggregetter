using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Threading;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Base
{
    public sealed class ValidatorMocks
    {
        public static Mock<AbstractValidator<T>> GetValidator<T>()
        {
            var mockValidator = new Mock<AbstractValidator<T>>();

            mockValidator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<T>>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                (ValidationContext<T> context, CancellationToken cancellationToken) =>
                {
                    return new ValidationResult();
                });

            return mockValidator;
        }
    }
}
