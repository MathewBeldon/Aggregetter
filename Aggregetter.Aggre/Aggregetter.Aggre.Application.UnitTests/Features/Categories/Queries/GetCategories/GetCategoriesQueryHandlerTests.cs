using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories;
using Aggregetter.Aggre.Application.Profiles;
using Aggregetter.Aggre.Application.UnitTests.Features.Base;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Categories.Queries.GetCategories
{
    public sealed class GetCategoriesQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBaseRepository<Category>> _mockArticleRepository;

        private readonly GetCategoriesQueryHandler _handler;

        public GetCategoriesQueryHandlerTests()
        {
            _mockArticleRepository = BaseRepositoryMocks<Category>.GetBaseRepositoryMocks();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CategoryMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _handler = new GetCategoriesQueryHandler(_mapper, _mockArticleRepository.Object);
        }

        [Fact]
        public async Task GetCategoriesQueryHandler_ReturnsCategories()
        {
            var result = await _handler.Handle(new GetCategoriesQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetCategoriesQueryResponse>();
            //result.Data.ShouldNotBeEmpty();
        }
    }
}
