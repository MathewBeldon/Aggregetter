using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Aggregetter.Aggre.Application.Services.PaginationService
{
    public class PaginationValidatorBase<T> : AbstractValidator<T> where T : PaginationRequest
    {
        public PaginationValidatorBase(IOptions<PagedSettings> settings)
        {
            RuleFor(pr => pr.PageSize)
                .InclusiveBetween(1, settings.Value.PageSize)
                .WithMessage($"Page size must be between 1 and {settings.Value.PageSize}");

            RuleFor(pr => pr.Page)
                .GreaterThan(0)
                .WithMessage("Page must be greater than 0");
        }
    }
}
