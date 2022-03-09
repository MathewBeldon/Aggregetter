using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Profiles;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticles
{
    public sealed class GetArticlesQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _mockArticleRepository;

        private readonly GetArticlesQueryHandler _handler;

        public GetArticlesQueryHandlerTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _handler = new GetArticlesQueryHandler(_mapper, _mockArticleRepository.Object);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetArticlePagedListQueryHandler_PageSizeOfInput_CorrectPageSize(int pageSize)
        {
            var result = await _handler.Handle(new GetArticlesQuery()
            {
                Page = 1,
                PageSize = pageSize
            }, CancellationToken.None);

            result.Should().BeOfType<GetArticlesQueryResponse>();
            result.Data.Count.Should().Be(pageSize);
        }

        [Fact]
        public async Task GetArticlePagedListQueryHandler_OutOfBoundsPage_NoResults()
        {
            var result = await _handler.Handle(new GetArticlesQuery()
            {
                Page = 999,
                PageSize = 20
            }, CancellationToken.None);

            result.Should().BeOfType<GetArticlesQueryResponse>();
            result.Data.Should().BeEmpty();
        }
    }
}
