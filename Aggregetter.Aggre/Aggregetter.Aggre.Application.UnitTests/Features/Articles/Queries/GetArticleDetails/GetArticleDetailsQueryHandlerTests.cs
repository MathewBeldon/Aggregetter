using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Profiles;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Net;
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
            var article = (await _mockArticleRepository.Object.GetArticlesPagedAsync(1, 1, totalArticles, CancellationToken.None)).FirstOrDefault();

            var result = await _handler.Handle(new GetArticleDetailsQuery
            {
                ArticleSlug = article.ArticleSlug
            }, CancellationToken.None);

            result.Should().BeOfType<GetArticleDetailsQueryResponse>();
            result.Data.ArticleSlug.Should().Be(article.ArticleSlug);
            result.Data.Category.Should().NotBeNull();
            result.Data.Provider.Should().NotBeNull();
            result.Data.CreatedDateUtc.Should().BeAfter(System.DateTime.MinValue);
            result.Data.Endpoint.Should().NotBeNull();
            result.Data.OriginalBody.Should().NotBeNull();
            result.Data.OriginalTitle.Should().NotBeNull();
            result.Data.TranslatedBody.Should().NotBeNull();
            result.Data.TranslatedTitle.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
