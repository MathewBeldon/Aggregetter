using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList;
using Aggregetter.Aggre.Application.Profiles;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
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
        private readonly Mock<IValidator<GetArticlePagedListQuery>> _validator;
        private readonly Mock<IConfiguration> _configuration;

        public GetArticlePagedListQueryHandlerTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();
            _validator = new Mock<IValidator<GetArticlePagedListQuery>>();
            _configuration = new Mock<IConfiguration>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetArticlePagedListTest()
        {
            var handler = new GetArticlePagedListQueryHandler(_mockArticleRepository.Object, _mapper, _validator.Object, _configuration.Object);
            var result = await handler.Handle(new GetArticlePagedListQuery(){
                page = 1
            }, CancellationToken.None);

            result.ShouldBeOfType<GetArticlePagedListQueryResponse>();
            result.Data.ShouldNotBeEmpty();
        }
    }
}
