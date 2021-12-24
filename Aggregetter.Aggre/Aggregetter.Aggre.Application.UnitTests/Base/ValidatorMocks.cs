using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList;
using Aggregetter.Aggre.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Aggregetter.Aggre.Application.UnitTests.Base
{
    public class ValidatorMocks
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
