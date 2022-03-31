using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults
{
    public sealed class GetArticleSearchResultsQueryValidator : PaginationValidatorBase<GetArticleSearchResultsQuery>
    {
        public GetArticleSearchResultsQueryValidator(IOptions<PagedSettings> settings) : base(settings)
        {
            RuleFor(query => query.SearchString)
                .NotEmpty()
                .WithMessage("Enter a search term");

            RuleFor(query => query.SearchString)
                .MinimumLength(3)
                .WithMessage("Search term must be 3 characters or more");
        }
    }
}