using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base
{
    public sealed class GetArticlesQueryValidator : PaginationValidatorBase<GetArticlesQuery>
    {
        public GetArticlesQueryValidator(IOptions<PagedSettings> settings) : base(settings)
        {
        }
    }
}
