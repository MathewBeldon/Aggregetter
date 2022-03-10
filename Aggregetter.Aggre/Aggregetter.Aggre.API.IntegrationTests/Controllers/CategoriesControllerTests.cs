using Aggregetter.Aggre.API.IntegrationTests.Base;
using Aggregetter.Aggre.Application.Features.Providers.Queries.GetProviders;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.API.IntegrationTests.Controllers
{
    public class CategoriesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public CategoriesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetCategories_v1_ValidRequest_Success()
        {
            var categoriesResponse = await _client.GetAsync("/api/v1/categories");
            categoriesResponse.EnsureSuccessStatusCode();

            var categoriesResponseString = await categoriesResponse.Content.ReadAsStringAsync();
            var categoriesResponseObject = JsonConvert.DeserializeObject<GetProvidersQueryResponse>(categoriesResponseString) ?? throw new ArgumentNullException();

            categoriesResponseObject.Should().NotBeNull();
            categoriesResponseObject.Data.Count.Should().BeGreaterThan(0);
        }
    }
}