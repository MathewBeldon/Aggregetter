using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Models.Pagination;
using Aggregetter.Aggre.Application.Profiles;
using Aggregetter.Aggre.Application.Services.PaginationService;
using AutoMapper;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries.GetArticles
{
    public sealed class GetArticlesQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _mockArticleRepository;
        private readonly Mock<IPaginationService> _paginationService;

        private readonly GetArticlesQueryHandler _handler;

        public GetArticlesQueryHandlerTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();
            _paginationService = new Mock<IPaginationService>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _handler = new GetArticlesQueryHandler(_mockArticleRepository.Object, _mapper, _paginationService.Object);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetArticlePagedListQueryHandler_PageSizeOfInput_CorrectPageSize(int pageSize)
        {
            var result = await _handler.Handle(new GetArticlesQuery(){
                PaginationRequest = new PaginationRequest
                {
                    Page = 1,
                    PageSize = pageSize
                }
            }, CancellationToken.None);

            result.ShouldBeOfType<GetArticlesQueryResponse>();
            result.Data.Count.ShouldBe(pageSize);
        }

        [Fact]
        public async Task GetArticlePagedListQueryHandler_OutOfBoundsPage_NoResults()
        {
            var result = await _handler.Handle(new GetArticlesQuery()
            {
                PaginationRequest = new PaginationRequest
                {
                    Page = 999,
                    PageSize = 20
                }
            }, CancellationToken.None);

            result.ShouldBeOfType<GetArticlesQueryResponse>();
            result.Message.ShouldBe("Page contains no data");
            result.Data.ShouldBeEmpty();
        }
    }
}
