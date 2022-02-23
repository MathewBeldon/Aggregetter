using Aggregetter.Aggre.API.IntegrationTests.Base.Seeds;
using Aggregetter.Aggre.Application.Models.Authentication;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.IntegrationTests.Base
{
    internal class AuthenticationHelper
    {
        public async static Task<AuthenticationResponse> LoginBasicUserAsync(HttpClient client, int version)
        {
            var authenticationRequest = new AuthenticationRequest()
            {
                Email = UserData.Email,
                Password = UserData.Password
            };

            var json = JsonConvert.SerializeObject(authenticationRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response =  await client.PostAsync($"/api/v{version}/accounts/authenticate", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(responseString);

            if (authenticationResponse is not null)
            {
                return authenticationResponse; 
            }

            return new AuthenticationResponse();
        }

        public async static Task LogoutUserAsync(HttpClient client, int version)
        {
            var response = await client.PostAsync($"/api/v{version}/accounts/deauthenticate", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
