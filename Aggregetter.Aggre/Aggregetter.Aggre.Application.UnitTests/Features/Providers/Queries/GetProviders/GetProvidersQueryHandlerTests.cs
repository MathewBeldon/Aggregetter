using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Providers.Queries.GetProviders;
using Aggregetter.Aggre.Application.Profiles;
using Aggregetter.Aggre.Application.UnitTests.Features.Base;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Providers.Queries.GetProviders
{
    public sealed class GetProvidersQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBaseRepository<Provider>> _mockArticleRepository;

        private readonly GetProvidersQueryHandler _handler;

        public GetProvidersQueryHandlerTests()
        {
            _mockArticleRepository = BaseRepositoryMocks<Provider>.GetBaseRepositoryMocks();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProviderMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _handler = new GetProvidersQueryHandler(_mapper, _mockArticleRepository.Object);
        }

        [Fact]
        public async Task GetProvidersQueryHandler_ReturnsProviders()
        {
            var result = await _handler.Handle(new GetProvidersQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetProvidersQueryResponse>();
            result.Data.ShouldNotBeEmpty();
        }
    }
}
