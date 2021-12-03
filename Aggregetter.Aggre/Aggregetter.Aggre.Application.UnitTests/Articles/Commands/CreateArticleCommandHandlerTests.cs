using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Profiles;
using AutoMapper;
using Moq;
using System;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Articles.Commands
{
    public class CreateArticleCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _mockArticleRepository;

        public CreateArticleCommandHandlerTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();

            var mappingProfile = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            _mapper = mappingProfile.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidArticle_AdddedToArticleRepository()
        {
            var handler = new CreateArticleCommandHandler(_mockArticleRepository.Object, _mapper);
            var articleCount = (await _mockArticleRepository.Object.GetAllAsync(CancellationToken.None)).Count();

            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = Guid.Parse("0763EBF37CC443A3B3AFD7F94109934C"),
                ProviderId = Guid.Parse("03867E6157024CBF9403716F4F900519"),
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint"
            };

            var response = await handler.Handle(createArticleCommand, CancellationToken.None);
            var allArticles = await _mockArticleRepository.Object.GetAllAsync(CancellationToken.None);

            response.Success.ShouldBeTrue();
            allArticles.Count.ShouldBe(articleCount + 1);
        }

        [Fact]
        public async Task Handle_InvalidArticle_AdddedToArticleRepository()
        {
            var handler = new CreateArticleCommandHandler(_mockArticleRepository.Object, _mapper);
            var existingArticle = (await _mockArticleRepository.Object.GetAllAsync(CancellationToken.None)).FirstOrDefault();
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = existingArticle.CategoryId,
                ProviderId = existingArticle.ProviderId,
                OriginalTitle = existingArticle.OriginalTitle,
                TranslatedTitle = existingArticle.TranslatedTitle,
                OriginalBody = existingArticle.OriginalBody,
                TranslatedBody = existingArticle.TranslatedBody,
                Endpoint = existingArticle.Endpoint
            };

            var response = await handler.Handle(createArticleCommand, CancellationToken.None);

            response.Success.ShouldBeFalse();
            response.ValidationErrors.Count().ShouldBe(1);
            response.ShouldBeOfType<CreateArticleCommandResponse>();
        }
    }
}
