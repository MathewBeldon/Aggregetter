using Aggregetter.Aggre.API.IntegrationTests.Base.Seeds;
using Aggregetter.Aggre.Application.Models.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.IntegrationTests.Base
{
    internal class AuthenticationHelper
    {
        public async static Task<AuthenticationResponse> LoginBasicUserAsync(HttpClient client)
        {
            var authenticationRequest = new AuthenticationRequest()
            {
                Email = UserData.Email,
                Password = UserData.Password
            };

            var json = JsonConvert.SerializeObject(authenticationRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response =  await client.PostAsync("/api/account/authenticate", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(responseString);

            if (authenticationResponse is not null)
            {
                return authenticationResponse; 
            }

            return new AuthenticationResponse();
        }

        public async static Task LogoutUserAsync(HttpClient client)
        {
            var response = await client.PostAsync("/api/account/deauthenticate", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
