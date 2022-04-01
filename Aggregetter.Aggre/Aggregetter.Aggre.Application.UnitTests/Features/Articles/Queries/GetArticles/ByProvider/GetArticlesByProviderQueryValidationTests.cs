using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider;
using Aggregetter.Aggre.Application.Settings;
using Aggregetter.Aggre.Application.UnitTests.Features.Base;
using Aggregetter.Aggre.Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;

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
    }
}
