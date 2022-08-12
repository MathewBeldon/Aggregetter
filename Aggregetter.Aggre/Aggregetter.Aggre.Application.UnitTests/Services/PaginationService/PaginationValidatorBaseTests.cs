using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Services.PaginationService
{
    public sealed class PaginationValidatorBaseTests
    {
        private readonly PaginationValidatorBase<IPaginationRequest> _validator;
        private readonly IOptions<PagedSettings> _options;

        private const int PAGE_SIZE = 20;

        public PaginationValidatorBaseTests()
        {
            _options = Options.Create(new PagedSettings
            {
                PageSize = PAGE_SIZE,
            });

            _validator = new PaginationValidatorBase<IPaginationRequest>(_options);
        }

        [Theory]
        [InlineData(1, 20)]
        [InlineData(1, 1)]
        [InlineData(999, 20)]
        [InlineData(999, 1)]
        public async Task PaginationValidator_ValidPageRequest_IsValid(int page, int pageSize)
        {
            var paginationRequest = new GetArticlesQuery
            {
                Page = page,
                PageSize = pageSize,
            };

            var result = await _validator.ValidateAsync(paginationRequest);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(21)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task PaginationValidator_InvalidPageSize_IsNotValid(int pageSize)
        {
            var paginationRequest = new GetArticlesQuery
            {
                Page = 1,
                PageSize = pageSize,
            };

            var result = await _validator.ValidateAsync(paginationRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task PaginationValidator_InvalidPage_IsNotValid(int page)
        {
            var paginationRequest = new GetArticlesQuery
            {
                Page = page,
                PageSize = PAGE_SIZE,
            };

            var result = await _validator.ValidateAsync(paginationRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }
    }
}
