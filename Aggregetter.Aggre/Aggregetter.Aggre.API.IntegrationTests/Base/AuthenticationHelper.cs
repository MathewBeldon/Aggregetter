using Aggregetter.Aggre.API.IntegrationTests.Base.Seeds;
using Aggregetter.Aggre.Application.Features.Accounts.Queries.AuthenticateAccount;
using Aggregetter.Aggre.Application.Models.Authentication;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.IntegrationTests.Base
{
    internal class AuthenticationHelper
    {
        public async static Task<AuthenticateAccountQueryResponse> LoginBasicUserAsync(HttpClient client, int version)
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
            var authenticationResponse = JsonConvert.DeserializeObject<AuthenticateAccountQueryResponse>(responseString);

            if (authenticationResponse is not null)
            {
                return authenticationResponse; 
            }

            throw new System.Exception();
        }

        public async static Task LogoutUserAsync(HttpClient client, int version)
        {
            var response = await client.PostAsync($"/api/v{version}/accounts/deauthenticate", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
