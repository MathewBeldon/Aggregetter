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

        [Fact]
        public void GetPagedUris_ValidInput_ValidResponse()
        {
            var result = _paginationService.GetPagedUris(20, 20, 20);

            result.NextPage.ShouldBeTrue();
        }
    }
}
