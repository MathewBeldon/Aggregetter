using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory
{
    public sealed class GetArticlesByCategoryQueryValidator : PaginationValidatorBase<GetArticlesByCategoryQuery>
    {
        public GetArticlesByCategoryQueryValidator(IOptions<PagedSettings> settings) : base(settings)
        {
        }
    }
}
