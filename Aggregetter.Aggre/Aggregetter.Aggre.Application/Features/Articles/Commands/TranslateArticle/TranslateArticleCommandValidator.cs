using Aggregetter.Aggre.Application.Contracts.Persistence;
using FluentValidation;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.TranslateArticle
{
    public sealed class TranslateArticleCommandValidator : AbstractValidator<TranslateArticleCommand>
    {
        readonly IArticleRepository _articleRepository;

        public TranslateArticleCommandValidator(IArticleRepository articleRepository)
        {
            ArgumentNullException.ThrowIfNull(articleRepository);

            _articleRepository = articleRepository;

            RuleFor(article => article.ArticleSlug)
                .MustAsync(async (articleSlug, cancellationToken) => {
                    return await _articleRepository.GetArticleBySlugAsync(articleSlug, cancellationToken) is not null;
                }).WithMessage("Article does not exist");
        }
    }
}
