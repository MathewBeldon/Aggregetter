﻿using Aggregetter.Aggre.API.IntegrationTests.Base;
using Aggregetter.Aggre.API.IntegrationTests.Base.Seeds;
using Aggregetter.Aggre.Application.Models.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.API.IntegrationTests.Controllers
{
    public sealed class AccountsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public AccountsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task PostAccount_v1_Register_Success()
        {
            var registrationRequest = new RegistrationRequest()
            {
                Email = "user@email.com",
                Username = "testUserName",
                Password = "TestUserPassword1$"
            };

            var json = JsonConvert.SerializeObject(registrationRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/accounts/register", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegistrationResponse>(responseString);

            Assert.IsType<RegistrationResponse>(result);
            Assert.NotNull(result?.UserId);
        }

        [Fact]
        public async Task PostAccount_v1_Authenticate_Success()
        {
            var result = await AuthenticationHelper.LoginBasicUserAsync(_client, version: 1);            

            Assert.IsType<AuthenticationResponse>(result);
            Assert.NotNull(result?.UserId);
        }
    }
}