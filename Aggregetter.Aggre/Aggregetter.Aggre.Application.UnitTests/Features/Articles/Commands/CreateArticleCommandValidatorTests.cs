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

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Commands
{
    public sealed class CreateArticleCommandValidatorTests
    {
        private CreateArticleCommandValidator _validator;
        private readonly Mock<IArticleRepository> _mockArticleRepository;

        public CreateArticleCommandValidatorTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();
            _validator = new CreateArticleCommandValidator(_mockArticleRepository.Object);
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
        public async Task CreateArticleCommandValidator_InvalidArticle_Fails()
        {
            var existingArticle = ArticleRepositoryMocks.articles.FirstOrDefault();
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = existingArticle.CategoryId,
                ProviderId = existingArticle.ProviderId,
                OriginalTitle = existingArticle.OriginalTitle,
                TranslatedTitle = existingArticle.TranslatedTitle,
                OriginalBody = existingArticle.OriginalBody,
                TranslatedBody = existingArticle.TranslatedBody,
                Endpoint = existingArticle.Endpoint,
                ArticleSlug = existingArticle.ArticleSlug
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
        }
    }
}
