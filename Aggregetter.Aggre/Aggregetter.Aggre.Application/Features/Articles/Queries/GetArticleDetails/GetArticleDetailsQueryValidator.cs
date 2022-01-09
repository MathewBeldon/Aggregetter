using FluentValidation;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsQueryValidator : AbstractValidator<GetArticleDetailsQuery>
    {
        public GetArticleDetailsQueryValidator()
        {
            RuleFor(ad => ad.ArticleSlug)
                .NotEmpty()
                .WithMessage("<slug> cannot be empty");
        }
    }
}
