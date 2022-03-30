using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider;
using Aggregetter.Aggre.Application.Settings;
using Aggregetter.Aggre.Application.UnitTests.Features.Base;
using Aggregetter.Aggre.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticles.ByProvider
{
    public sealed class GetArticlesByProviderQueryValidationTests
    {
        private readonly GetArticlesByProviderQueryValidator _validator;
        private readonly IOptions<PagedSettings> _options;
        private readonly Mock<IBaseRepository<Provider>> _mockProviderRepository;

        private const int PAGE_SIZE = 20;

        public GetArticlesByProviderQueryValidationTests()
        {
            _mockProviderRepository = BaseRepositoryMocks<Provider>.GetBaseRepositoryMocks();

            _options = Options.Create(new PagedSettings
            {
                PageSize = PAGE_SIZE,
            });

            _validator = new GetArticlesByProviderQueryValidator(_mockProviderRepository.Object, _options);
        }

        [Theory]
        [InlineData(1, 20)]
        [InlineData(1, 1)]
        [InlineData(999, 20)]
        [InlineData(999, 1)]
        public async Task GetArticlePagedListQueryValidator_ValidPageRequest_IsValid(int page, int pageSize)
        {
            var getArticlePagedListQuery = new GetArticlesByProviderQuery
            {
                ProviderId = 1,
                Page = page,
                PageSize = pageSize,
            };

            var result = await _validator.ValidateAsync(getArticlePagedListQuery);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(21)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetArticlePagedListQueryValidator_InvalidPageSize_IsNotValid(int pageSize)
        {
            var getArticlesQuery = new GetArticlesByProviderQuery
            {
                ProviderId = 1,
                Page = 1,
                PageSize = pageSize,
            };

            var result = await _validator.ValidateAsync(getArticlesQuery);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetArticlePagedListQueryValidator_InvalidPage_IsNotValid(int page)
        {
            var getArticlePagedListQuery = new GetArticlesByProviderQuery
            {
                ProviderId = 1,
                Page = page,
                PageSize = PAGE_SIZE,
            };

            var result = await _validator.ValidateAsync(getArticlePagedListQuery);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }
    }
}
