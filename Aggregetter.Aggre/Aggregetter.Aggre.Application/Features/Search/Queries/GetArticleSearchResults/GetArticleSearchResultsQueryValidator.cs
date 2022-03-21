﻿using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Features.Search.Queries.GetArticleSearchResults
{
    public sealed class GetArticleSearchResultsQueryValidator : AbstractValidator<GetArticleSearchResultsQuery>
    {
        public GetArticleSearchResultsQueryValidator(IOptions<PagedSettings> settings)
        {
            RuleFor(query => query.PageSize)
                .InclusiveBetween(1, settings.Value.PageSize)
                .WithMessage($"Page size should be between 1 and {settings.Value.PageSize}");

            RuleFor(query => query.Page)
                .GreaterThan(0)
                .WithMessage("Page should be greater than 0");

            RuleFor(query => query.SearchString)
                .NotEmpty()
                .WithMessage("Search cannot be empty");

            RuleFor(query => query.SearchString)
                .MinimumLength(3)
                .WithMessage("Search must be at least 3 characters");
        }
    }
}