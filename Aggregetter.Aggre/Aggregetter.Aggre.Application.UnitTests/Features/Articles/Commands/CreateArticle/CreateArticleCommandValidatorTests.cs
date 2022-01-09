﻿using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.UnitTests.Features.Base;
using Aggregetter.Aggre.Domain.Entities;
using Moq;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Commands.CreateArticle
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
                CategoryId = BaseRepositoryMocks<Category>.ExistingId,
                ProviderId = BaseRepositoryMocks<Provider>.ExistingId,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint/ValidArticle",
                ArticleSlug = "valid-article"
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task CreateArticleCommandValidator_InvalidCategory_Fails()
        {
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = -1,
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
                ProviderId = -1,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint/InvalidProvider",
                ArticleSlug = "invalid-provider"
            };
            
            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors.FirstOrDefault().PropertyName.ShouldBe(nameof(CreateArticleCommand.ProviderId));
        }

        [Fact]
        public async Task CreateArticleCommandValidator_InvalidEndpoint_Fails()
        {
            var takenEndpoint = (await _mockArticleRepository.Object.GetPagedResponseAsync(1, 1, CancellationToken.None)).FirstOrDefault().Endpoint;
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = BaseRepositoryMocks<Category>.ExistingId,
                ProviderId = BaseRepositoryMocks<Provider>.ExistingId,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = takenEndpoint,
                ArticleSlug = "invalid-endpoint"
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors.FirstOrDefault().PropertyName.ShouldBe(nameof(CreateArticleCommand.Endpoint));
        }

        [Fact]
        public async Task CreateArticleCommandValidator_InvalidArticleSlug_Fails()
        {
            var takenArticleSlug = (await _mockArticleRepository.Object.GetPagedResponseAsync(1, 1, CancellationToken.None)).FirstOrDefault().ArticleSlug;
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = BaseRepositoryMocks<Category>.ExistingId,
                ProviderId = BaseRepositoryMocks<Provider>.ExistingId,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint/InvalidArticleSlug",
                ArticleSlug = takenArticleSlug
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors.FirstOrDefault().PropertyName.ShouldBe(nameof(CreateArticleCommand.ArticleSlug));
        }

        [Fact]
        public async Task CreateArticleCommandValidator_InvalidOriginalTitle_Fails()
        {
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = BaseRepositoryMocks<Category>.ExistingId,
                ProviderId = BaseRepositoryMocks<Provider>.ExistingId,
                OriginalTitle = string.Empty,
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint/InvalidOriginalTitle",
                ArticleSlug = "invalid-original-title"
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors.FirstOrDefault().PropertyName.ShouldBe(nameof(CreateArticleCommand.OriginalTitle));
        }

        [Fact]
        public async Task CreateArticleCommandValidator_InvalidTranslatedTitle_Fails()
        {
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = BaseRepositoryMocks<Category>.ExistingId,
                ProviderId = BaseRepositoryMocks<Provider>.ExistingId,
                OriginalTitle = "OriginalTitle",
                TranslatedTitle = string.Empty,
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint/InvalidTranslatedTitle",
                ArticleSlug = "invalid-translated-title"
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors.FirstOrDefault().PropertyName.ShouldBe(nameof(CreateArticleCommand.TranslatedTitle));
        }

        [Fact]
        public async Task CreateArticleCommandValidator_InvalidOriginalBody_Fails()
        {
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = BaseRepositoryMocks<Category>.ExistingId,
                ProviderId = BaseRepositoryMocks<Provider>.ExistingId,
                OriginalTitle = "OriginalTitle",
                TranslatedTitle = "TranslatedTitle",
                OriginalBody = string.Empty,
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint/InvalidOriginalBody",
                ArticleSlug = "invalid-original-body"
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors.FirstOrDefault().PropertyName.ShouldBe(nameof(CreateArticleCommand.OriginalBody));
        }

        [Fact]
        public async Task CreateArticleCommandValidator_InvalidTranslatedBody_Fails()
        {
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = BaseRepositoryMocks<Category>.ExistingId,
                ProviderId = BaseRepositoryMocks<Provider>.ExistingId,
                OriginalTitle = "OriginalTitle",
                TranslatedTitle = "TranslatedTitle",
                OriginalBody = "Original Body",
                TranslatedBody = string.Empty,
                Endpoint = "New/Endpoint/InvalidTranslatedBody",
                ArticleSlug = "invalid-translated-body"
            };

            var result = await _validator.ValidateAsync(createArticleCommand, CancellationToken.None);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);
            result.Errors.FirstOrDefault().PropertyName.ShouldBe(nameof(CreateArticleCommand.TranslatedBody));
        }
    }
}
