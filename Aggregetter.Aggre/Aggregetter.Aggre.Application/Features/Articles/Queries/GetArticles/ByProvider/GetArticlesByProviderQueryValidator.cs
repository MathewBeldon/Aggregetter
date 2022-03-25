using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider
{
    public sealed class GetArticlesByProviderQueryValidator : PaginationValidatorBase<GetArticlesByProviderQuery>
    {
        public GetArticlesByProviderQueryValidator(IOptions<PagedSettings> settings) : base(settings)
        {
        }
    }
}
