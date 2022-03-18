using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQueryValidator : AbstractValidator<GetArticlesQuery>
    {
        public GetArticlesQueryValidator(IOptions<PagedSettings> settings)
        {
            RuleFor(pr => pr.PageSize)
                .InclusiveBetween(1, settings.Value.PageSize)
                .WithMessage($"Page size should be between 1 and {settings.Value.PageSize}");

            RuleFor(pr => pr.Page)
                .GreaterThan(0)
                .WithMessage("Page should be greater than 0");
        }
    }
}
