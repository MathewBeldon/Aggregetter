using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory
{
    public sealed class GetArticlesByCategoryQueryValidator : AbstractValidator<GetArticlesByCategoryQuery>
    {
        public GetArticlesByCategoryQueryValidator(IOptions<PagedSettings> settings)
        {
            RuleFor(pr => pr.PaginationRequest.PageSize)
                .InclusiveBetween(1, settings.Value.PageSize)
                .WithMessage($"Page size should be between 1 and {settings.Value.PageSize}");

            RuleFor(pr => pr.PaginationRequest.Page)
                .GreaterThan(0)
                .WithMessage("<page> should be greater than 0");
        }
    }
}
