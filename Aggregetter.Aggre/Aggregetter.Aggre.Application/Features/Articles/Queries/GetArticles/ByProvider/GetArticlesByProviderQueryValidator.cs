using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider
{
    public sealed class GetArticlesByProviderQueryValidator : AbstractValidator<GetArticlesByProviderQuery>
    {
        public GetArticlesByProviderQueryValidator(IOptions<PagedSettings> settings)
        {
            RuleFor(pr => pr.PageSize)
                .InclusiveBetween(1, settings.Value.PageSize)
                .WithMessage($"Page size should be between 1 and {settings.Value.PageSize}");

            RuleFor(pr => pr.Page)
                .GreaterThan(0)
                .WithMessage("<page> should be greater than 0");
        }
    }
}
