using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory;
using Aggregetter.Aggre.Application.Settings;
using Microsoft.Extensions.Options;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticles.ByCategory
{
    public sealed class GetArticlesByCategoryQueryValidationTests
    {
        private readonly GetArticlesByCategoryQueryValidator _validator;
        private readonly IOptions<PagedSettings> _options;

        private const int PAGE_SIZE = 20;

        public GetArticlesByCategoryQueryValidationTests()
        {
            _options = Options.Create(new PagedSettings
            {
                PageSize = PAGE_SIZE,
            });

            _validator = new GetArticlesByCategoryQueryValidator(_options);
        }

        [Theory]
        [InlineData(1, 20)]
        [InlineData(1, 1)]
        [InlineData(999, 20)]
        [InlineData(999, 1)]
        public async Task GetArticlePagedListQueryValidator_ValidPageRequest_IsValid(int page, int pageSize)
        {
            var getArticlePagedListQuery = new GetArticlesByCategoryQuery
            {
                CategoryId = 1,
                Page = page,
                PageSize = pageSize,
            };

            var result = await _validator.ValidateAsync(getArticlePagedListQuery);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(21)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetArticlePagedListQueryValidator_InvalidPageSize_IsNotValid(int pageSize)
        {
            var getArticlesQuery = new GetArticlesByCategoryQuery
            {
                CategoryId = 1,
                Page = 1,
                PageSize = pageSize,
            };

            var result = await _validator.ValidateAsync(getArticlesQuery);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetArticlePagedListQueryValidator_InvalidPage_IsNotValid(int page)
        {
            var getArticlePagedListQuery = new GetArticlesByCategoryQuery
            {
                CategoryId = 1,
                Page = page,
                PageSize = PAGE_SIZE,
            };

            var result = await _validator.ValidateAsync(getArticlePagedListQuery);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
        }
    }
}
