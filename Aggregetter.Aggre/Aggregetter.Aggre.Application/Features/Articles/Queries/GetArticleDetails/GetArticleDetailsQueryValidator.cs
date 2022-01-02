using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
