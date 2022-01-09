using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles;
using Aggregetter.Aggre.Application.Requests;
using Aggregetter.Aggre.Application.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticles
{
    public sealed class GetArticlesQueryValidationTests
    {
        private readonly GetArticlesQueryValidator _validator;
        private readonly IOptions<PagedSettings> _options;

        private const int PAGE_SIZE = 20;
        public GetArticlesQueryValidationTests()
        {
            _options = Options.Create(new PagedSettings
            {
                PageSize = PAGE_SIZE,
            });

            _validator = new GetArticlesQueryValidator(_options);
        }


        [Theory]
        [InlineData(1, 20)]
        [InlineData(1, 1)]
        [InlineData(999, 20)]
        [InlineData(999, 1)]
        public async Task GetArticlePagedListQueryValidator_ValidPageRequest_IsValid(int page, int pageSize)
        {
            var getArticlePagedListQuery = new GetArticlesQuery
            {
                PagedRequest = new PagedRequest
                {
                    Page = page,
                    PageSize = pageSize,
                }
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
            var getArticlePagedListQuery = new GetArticlesQuery
            {
                PagedRequest = new PagedRequest
                {
                    Page = 1,
                    PageSize = pageSize,
                }
            };

            var result = await _validator.ValidateAsync(getArticlePagedListQuery);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetArticlePagedListQueryValidator_InvalidPage_IsNotValid(int page)
        {
            var getArticlePagedListQuery = new GetArticlesQuery
            {
                PagedRequest = new PagedRequest
                {
                    Page = page,
                    PageSize = PAGE_SIZE,
                }
            };

            var result = await _validator.ValidateAsync(getArticlePagedListQuery);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
        }
    }
}
