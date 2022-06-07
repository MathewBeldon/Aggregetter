using Paging = Aggregetter.Aggre.Application.Services.PaginationService;
using FluentAssertions;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Services.PaginationServices
{
    public sealed class PaginationServiceTests
    {
        private readonly Paging.IPaginationService _paginationService;

        public PaginationServiceTests()
        {
            _paginationService = new Paging.PaginationService();
        }

        [Theory]
        [InlineData(20, 1, 100, true, false)]
        [InlineData(20, 2, 100, true, true)]
        [InlineData(20, 1, 20, false, false)]
        [InlineData(20, 2, 40, false, true)]
        public void GetPagedUris_ValidInput_ValidResponse(int pageSize, int page, int totalRecords, 
            bool expectedNextPageResult, bool expectedPreviousPageResult)
        {
            var (PreviousPage, NextPage) = _paginationService.GetPagedUris(pageSize, page, totalRecords);

            NextPage.Should().Be(expectedNextPageResult);
            PreviousPage.Should().Be(expectedPreviousPageResult);
        }
    }
}
