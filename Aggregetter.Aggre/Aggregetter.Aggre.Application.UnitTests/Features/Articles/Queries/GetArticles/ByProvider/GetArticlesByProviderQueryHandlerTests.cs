using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.ByProvider;
using Aggregetter.Aggre.Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticles.ByProvider
{
    public sealed class GetArticlesByProviderQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _mockArticleRepository;

        private readonly GetArticlesByProviderQueryHandler _handler;

        public GetArticlesByProviderQueryHandlerTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _handler = new GetArticlesByProviderQueryHandler(_mapper, _mockArticleRepository.Object);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetArticlesByCategoryQueryHandler_PageSizeOfInput_CorrectPageSize(int pageSize)
        {
            var result = await _handler.Handle(new GetArticlesByProviderQuery()
            {
                ProviderId = 1,
                Page = 1,
                PageSize = pageSize
            }, CancellationToken.None);

            result.ShouldBeOfType<GetArticlesQueryResponse>();
            result.Data.Count.ShouldBe(pageSize);
        }

        [Fact]
        public async Task GetArticlesByCategoryQueryHandler_OutOfBoundsPage_NoResults()
        {
            var result = await _handler.Handle(new GetArticlesByProviderQuery()
            {
                ProviderId = 1,
                Page = 999,
                PageSize = 20
            }, CancellationToken.None);

            result.ShouldBeOfType<GetArticlesQueryResponse>();
            result.Data.ShouldBeEmpty();
        }
    }
}
