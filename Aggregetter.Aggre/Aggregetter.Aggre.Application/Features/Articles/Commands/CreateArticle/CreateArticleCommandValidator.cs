using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using FluentValidation;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        readonly IArticleRepository _articleRepository;
        readonly IBaseRepository<Category> _categoryRepository;
        readonly IBaseRepository<Provider> _providerRepository;


        public CreateArticleCommandValidator(IArticleRepository articleRepository,
                                             IBaseRepository<Category> categoryRepository,
                                             IBaseRepository<Provider> providerRepository)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _providerRepository = providerRepository;

            RuleFor(article => article.CategoryId)
                .MustAsync(async (categoryId, cancellationToken) => {
                    return await _categoryRepository.GetByIdAsync(categoryId, cancellationToken) is not null;
                }).WithMessage("Category does not exist");

            RuleFor(article => article.ProviderId)
                .MustAsync(async (providerId, cancellationToken) => {
                    return await _providerRepository.GetByIdAsync(providerId, cancellationToken) is not null;
                }).WithMessage("Provider does not exist");

            RuleFor(article => article.Endpoint)
                .MustAsync(async (endpoint, cancellationToken) => {
                    return !await _articleRepository.ArticleEndpointExistsAsync(endpoint, cancellationToken);
                }).WithMessage("An article with this endpoint exists already");

            RuleFor(article => article.ArticleSlug)
                .MustAsync(async (articleSlug, cancellationToken) => {
                    return !await _articleRepository.ArticleSlugExistsAsync(articleSlug, cancellationToken);
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
