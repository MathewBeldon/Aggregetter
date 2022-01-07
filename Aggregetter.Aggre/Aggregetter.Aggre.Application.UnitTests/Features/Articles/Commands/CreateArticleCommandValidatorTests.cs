using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Aggregetter.Aggre.Domain.Entities;
using Aggregetter.Aggre.Application.UnitTests.Features.Base;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Commands
{
    public sealed class CreateArticleCommandValidatorTests
    {
        private CreateArticleCommandValidator _validator;
        private readonly Mock<IArticleRepository> _mockArticleRepository;
        private readonly Mock<IBaseRepository<Category>> _mockCategoryRepository;
        private readonly Mock<IBaseRepository<Provider>> _mockProviderRepository;


        public CreateArticleCommandValidatorTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();
            _mockCategoryRepository = BaseRepositoryMocks<Category>.GetBaseRepositoryMocks();
            _mockProviderRepository = BaseRepositoryMocks<Provider>.GetBaseRepositoryMocks();

            _validator = new CreateArticleCommandValidator(_mockArticleRepository.Object, _mockCategoryRepository.Object, _mockProviderRepository.Object);
        }


        [Fact]
        public async Task CreateArticleCommandValidator_ValidArticle_Passes()
        {
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = Guid.Parse("0763EBF37CC443A3B3AFD7F94109934C"),
                ProviderId = Guid.Parse("03867E6157024CBF9403716F4F900519"),
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint",
                ArticleSlug = "original-title"
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task CreateArticleCommandValidator_InvalidCategory_Fails()
        {
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = Guid.NewGuid(),
                ProviderId = BaseRepositoryMocks<Provider>.ExistingId,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint/InvalidCategory",
                ArticleSlug = "invalid-category"
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors.FirstOrDefault().PropertyName.ShouldBe(nameof(CreateArticleCommand.CategoryId));
        }

        [Fact]
        public async Task CreateArticleCommandValidator_InvalidProvider_Fails()
        {
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = BaseRepositoryMocks<Category>.ExistingId,
                ProviderId = Guid.NewGuid(),
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint/InvalidCategory",
                ArticleSlug = "invalid-category"
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors.FirstOrDefault().PropertyName.ShouldBe(nameof(CreateArticleCommand.ProviderId));
        }
    }
}
