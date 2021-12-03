using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleList;
using Aggregetter.Aggre.Application.Profiles;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Articles.Queries
{
    public class GetArticlePagedListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _mockArticleRepository;

        public GetArticlePagedListQueryHandlerTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetArticlePagedListTest()
        {
            var handler = new GetArticlePagedListQueryHandler(_mockArticleRepository.Object, _mapper);
            var result = await handler.Handle(new GetArticlePagedListQuery(){
                page = 1
            }, CancellationToken.None);

            result.ShouldBeOfType<List<ArticlePagedListVm>>();
            result.ShouldNotBeEmpty();
        }
    }
}
