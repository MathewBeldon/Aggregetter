using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProviderAndCategory
{
    public sealed class GetArticlesByProviderAndCategoryQueryValidator : PaginationValidatorBase<GetArticlesByProviderAndCategoryQuery>
    {
        public GetArticlesByProviderAndCategoryQueryValidator(IOptions<PagedSettings> settings) : base(settings)
        {
        }
    }
}
