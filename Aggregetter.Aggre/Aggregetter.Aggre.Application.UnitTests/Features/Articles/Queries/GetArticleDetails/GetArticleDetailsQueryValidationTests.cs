using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticleDetails
{
    public sealed class GetArticleDetailsQueryValidationTests
    {
        private readonly GetArticleDetailsQueryValidator _validator;

        public GetArticleDetailsQueryValidationTests()
        {
            _validator = new GetArticleDetailsQueryValidator();
        }

        [Theory]
        [InlineData("valid-slug")]
        [InlineData("12314123")]
        [InlineData("X")]
        public async Task GetArticleDetailsQueryValidator_ValidRequest_IsValid(string articleSlug)
        {
            var getArticlePagedListQuery = new GetArticleDetailsQuery
            {
                ArticleSlug = articleSlug
            };

            var result = await _validator.ValidateAsync(getArticlePagedListQuery);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public async Task GetArticleDetailsQueryValidator_ValidRequest_IsNotValid(string articleSlug)
        {
            var getArticlePagedListQuery = new GetArticleDetailsQuery
            {
                ArticleSlug = articleSlug
            };

            var result = await _validator.ValidateAsync(getArticlePagedListQuery);

            result.IsValid.Should().BeFalse();
        }
    }
}
