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
    public class ProvidersControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProvidersControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProviders_v1_ValidRequest_Success()
        {
            var providersResponse = await _client.GetAsync("/api/v1/providers");
            providersResponse.EnsureSuccessStatusCode();

            var providersResponseString = await providersResponse.Content.ReadAsStringAsync();
            var providersResponseObject = JsonConvert.DeserializeObject<GetProvidersQueryResponse>(providersResponseString) ?? throw new ArgumentNullException();

            providersResponseObject.Should().NotBeNull();
            providersResponseObject.Data.Count.Should().BeGreaterThan(0);
        }
    }
}