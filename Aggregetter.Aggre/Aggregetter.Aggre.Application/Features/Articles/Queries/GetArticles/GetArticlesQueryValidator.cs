using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles
{
    public sealed class GetArticlesQueryValidator : AbstractValidator<GetArticlesQuery>
    {
        public GetArticlesQueryValidator(IOptions<PagedSettings> settings)
        {
            RuleFor(pr => pr.PagedRequest.PageSize)
                .InclusiveBetween(1, settings.Value.PageSize)
                .WithMessage($"<pageSize> should be within 1 and {settings.Value.PageSize}");

            RuleFor(pr => pr.PagedRequest.Page)
                .GreaterThan(0)
                .WithMessage("<page> should be greater than 0");
        }
    }
}
