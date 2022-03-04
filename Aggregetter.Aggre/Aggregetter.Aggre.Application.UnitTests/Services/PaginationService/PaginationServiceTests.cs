using Aggregetter.Aggre.Application.Services.PaginationService;
using Shouldly;
using Xunit;

namespace Aggregetter.Aggre.Application.UnitTests.Services.PaginationServices
{
    public sealed class PaginationServiceTests
    {
        private readonly IPaginationService _paginationService;

        public PaginationServiceTests()
        {
            _paginationService = new PaginationService();
        }

        [Theory]
        [InlineData(20, 1, 100, true, false)]
        [InlineData(20, 2, 100, true, true)]
        [InlineData(20, 1, 20, false, false)]
        [InlineData(20, 2, 40, false, true)]
        public void GetPagedUris_ValidInput_ValidResponse(int pageSize, int page, int totalRecords, 
            bool expectedNextPageResult, bool expectedPreviousPageResult)
        {
            var result = _paginationService.GetPagedUris(pageSize, page, totalRecords);

            result.NextPage.ShouldBe(expectedNextPageResult);
            result.PreviousPage.ShouldBe(expectedPreviousPageResult);
        }
    }
}
