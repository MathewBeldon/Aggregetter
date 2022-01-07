using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList;
using Aggregetter.Aggre.Application.Profiles;
using Aggregetter.Aggre.Application.Requests;
using Aggregetter.Aggre.Application.Services.UriService;
using Aggregetter.Aggre.Application.UnitTests.Features.Base;
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

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles.Queries
{
    public class GetArticlePagedListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _mockArticleRepository;
        private readonly Mock<AbstractValidator<GetArticlePagedListQuery>> _validator;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IUriService> _uriService;

        private readonly GetArticlePagedListQueryHandler _handler;

        public GetArticlePagedListQueryHandlerTests()
        {
            _mockArticleRepository = ArticleRepositoryMocks.GetArticleRepository();
            _validator = ValidatorMocks.GetValidator<GetArticlePagedListQuery>();
            _configuration = new Mock<IConfiguration>();
            _uriService = new Mock<IUriService>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleMappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _handler = new GetArticlePagedListQueryHandler(_mockArticleRepository.Object, _mapper, _uriService.Object);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetArticlePagedListQueryHandler_PageSizeOfInput_CorrectPageSize(int pageSize)
        {
            var result = await _handler.Handle(new GetArticlePagedListQuery(){
                PagedRequest = new PagedRequest {
                    Page = 1,
                    PageSize = pageSize
                }
            }, CancellationToken.None);

            result.ShouldBeOfType<GetArticlePagedListQueryResponse>();
            result.Data.Count.ShouldBe(pageSize);
        }

        [Fact]
        public async Task GetArticlePagedListQueryHandler_OutOfBoundsPage_NoResults()
        {
            var result = await _handler.Handle(new GetArticlePagedListQuery()
            {
                PagedRequest = new PagedRequest
                {
                    Page = 999,
                    PageSize = 20
                }
            }, CancellationToken.None);

            result.ShouldBeOfType<GetArticlePagedListQueryResponse>();
            result.Message.ShouldBe("Page contains no data");
            result.Data.ShouldBeEmpty();
        }
    }
}
