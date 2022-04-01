using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByCategory;
using Aggregetter.Aggre.Application.Settings;
using Aggregetter.Aggre.Application.UnitTests.Features.Base;
using Aggregetter.Aggre.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticles.ByCategory
{
    public sealed class GetArticlesByCategoryQueryValidationTests
    {
        private readonly GetArticlesByCategoryQueryValidator _validator;
        private readonly IOptions<PagedSettings> _options;
        private readonly Mock<IBaseRepository<Category>> _mockCategoryRepository;

        private const int PAGE_SIZE = 20;

        public GetArticlesByCategoryQueryValidationTests()
        {
            _mockCategoryRepository = BaseRepositoryMocks<Category>.GetBaseRepositoryMocks();

            _options = Options.Create(new PagedSettings
            {
                PageSize = PAGE_SIZE,
            });

            _validator = new GetArticlesByCategoryQueryValidator(_mockCategoryRepository.Object, _options);
        }
    }
}
