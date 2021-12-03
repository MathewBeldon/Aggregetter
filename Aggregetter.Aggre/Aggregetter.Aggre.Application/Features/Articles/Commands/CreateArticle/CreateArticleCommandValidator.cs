using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        readonly IArticleRepository _articleRepository;

        public CreateArticleCommandValidator(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;

            RuleFor(article => article.Endpoint)
                .MustAsync(async (endpoint, cancellationToken) => {
                    return await _articleRepository.IsArticleEndpointUniqueAsync(endpoint, cancellationToken);
                }).WithMessage("An article with this endpoint exists already");

            RuleFor(article => article.OriginalTitle)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(article => article.TranslatedTitle)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(article => article.OriginalBody)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(article => article.TranslatedBody)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
