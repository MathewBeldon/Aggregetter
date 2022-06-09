using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Profiles;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _mockArticleRepository;
        private readonly CreateArticleCommandHandler _handler;

        public CreateArticleCommandHandlerTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();

            var mappingProfile = new MapperConfiguration(config =>
            {
                config.AddProfile<ArticleMappingProfile>();
            });

            _mapper = mappingProfile.CreateMapper();

            _handler = new CreateArticleCommandHandler(_mockArticleRepository.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ValidArticle_AddedToArticleRepository()
        {
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = 1,
                ProviderId = 1,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint",
                ArticleSlug = "original-title"
            };

            _ = await _handler.Handle(createArticleCommand, CancellationToken.None);
            var articleExists = await _mockArticleRepository.Object.ArticleSlugExistsAsync("original-title", CancellationToken.None);

            articleExists.Should().BeTrue();
        }       
    }
}
