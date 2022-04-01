using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProviderAndCategory;
using Aggregetter.Aggre.Application.Settings;
using Aggregetter.Aggre.Application.UnitTests.Features.Base;
using Aggregetter.Aggre.Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticles.ByProviderAndCategory
{
    public sealed class GetArticlesByProviderAndCategoryQueryValidationTests
    {
        private readonly GetArticlesByProviderAndCategoryQueryValidator _validator;
        private readonly IOptions<PagedSettings> _options;
        private readonly Mock<IBaseRepository<Provider>> _mockProviderRepository;
        private readonly Mock<IBaseRepository<Category>> _mockCategoryRepository;

        private const int PAGE_SIZE = 20;

        public GetArticlesByProviderAndCategoryQueryValidationTests()
        {
            _mockProviderRepository = BaseRepositoryMocks<Provider>.GetBaseRepositoryMocks();
            _mockCategoryRepository = BaseRepositoryMocks<Category>.GetBaseRepositoryMocks();

            _options = Options.Create(new PagedSettings
            {
                PageSize = PAGE_SIZE,
            });

            _validator = new GetArticlesByProviderAndCategoryQueryValidator(_mockProviderRepository.Object, _mockCategoryRepository.Object, _options);
        }
    }
}
