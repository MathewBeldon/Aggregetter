﻿using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticles
{
    public sealed class GetArticleDetailsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _mockArticleRepository;


        private readonly GetArticleDetailsQueryHandler _handler;

        public GetArticleDetailsQueryHandlerTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _handler = new GetArticleDetailsQueryHandler(_mockArticleRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetArticleDetailsQueryHandler_ValidRequest_ValidResponse()
        {
            var totalArticles = await _mockArticleRepository.Object.GetCount(CancellationToken.None);
            var article = (await _mockArticleRepository.Object.GetArticlesByPageAsync(1, 1, totalArticles, CancellationToken.None)).FirstOrDefault();

            var result = await _handler.Handle(new GetArticleDetailsQuery
            {
                ArticleSlug = article.ArticleSlug
            }, CancellationToken.None);

            result.ShouldBeOfType<GetArticleDetailsQueryResponse>();
            result.Data.ArticleSlug.ShouldBe(article.ArticleSlug);
            result.Data.Category.ShouldNotBeNull();
            result.Data.Provider.ShouldNotBeNull();
            result.Data.CreatedDateUtc.ShouldBeGreaterThan(System.DateTime.MinValue);
            result.Data.Endpoint.ShouldNotBeNull();
            result.Data.OriginalBody.ShouldNotBeNull();
            result.Data.OriginalTitle.ShouldNotBeNull();
            result.Data.TranslatedBody.ShouldNotBeNull();
            result.Data.TranslatedTitle.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
        }
    }
}
