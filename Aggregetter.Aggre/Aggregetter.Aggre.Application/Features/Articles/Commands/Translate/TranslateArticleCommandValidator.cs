using Aggregetter.Aggre.Application.Contracts.Persistence;
using FluentValidation;
using System;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.Translate
{
    public sealed class TranslateArticleCommandValidator : AbstractValidator<TranslateArticleCommand>
    {
        readonly IArticleRepository _articleRepository;

        public TranslateArticleCommandValidator(IArticleRepository articleRepository)
        {
            ArgumentNullException.ThrowIfNull(articleRepository);

            _articleRepository = articleRepository;

            RuleFor(article => article.ArticleId)
                .MustAsync(async (articleId, cancellationToken) => {
                    return await _articleRepository.GetByIdAsync(articleId, cancellationToken) is not null;
                }).WithMessage("Article does not exist");
        }
    }
}
