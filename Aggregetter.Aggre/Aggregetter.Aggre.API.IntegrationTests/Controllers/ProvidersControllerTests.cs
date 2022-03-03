﻿using Aggregetter.Aggre.API.IntegrationTests.Base;
using Aggregetter.Aggre.Application.Features.Providers.Queries.GetProviders;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.API.IntegrationTests.Controllers
{
    public class ProvidersControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public ProvidersControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetCategories_v1_ValidRequest_Success()
        {
            var categoriesResponse = await _client.GetAsync("/api/v1/providers");
            categoriesResponse.EnsureSuccessStatusCode();

            var categoriesResponseString = await categoriesResponse.Content.ReadAsStringAsync();
            var categoriesResponseObject = JsonConvert.DeserializeObject<GetProvidersQueryResponse>(categoriesResponseString) ?? throw new ArgumentNullException();

            categoriesResponseObject.ShouldNotBeNull();
            categoriesResponseObject.Data.Count.ShouldBeGreaterThan(0);
        }
    }
}