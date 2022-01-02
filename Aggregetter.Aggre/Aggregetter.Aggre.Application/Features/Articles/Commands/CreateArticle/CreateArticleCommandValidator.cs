using Aggregetter.Aggre.Application.Contracts.Persistence;
using FluentValidation;

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

            RuleFor(article => article.ArticleSlug)
                .MustAsync(async (articleSlug, cancellationToken) => {
                    return await _articleRepository.IsArticleEndpointUniqueAsync(articleSlug, cancellationToken);
                }).WithMessage("An article with this slug exists already");

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

            RuleFor(article => article.ArticleSlug)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
