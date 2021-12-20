using Aggregetter.Aggre.API.IntegrationTests.Base;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticlePagedList;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
namespace Aggregetter.Aggre.API.IntegrationTests.Controllers
{
    public class ArticleControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public ArticleControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetArticlesByPage_Success()
        {
            var articlesDefaultPage = await _client.GetAsync("/api/article/");
            articlesDefaultPage.EnsureSuccessStatusCode();

            var articlesFirstPage = await _client.GetAsync("/api/article/1");
            articlesFirstPage.EnsureSuccessStatusCode();

            var articlesSecondPage = await _client.GetAsync("/api/article/2");
            articlesSecondPage.EnsureSuccessStatusCode();

            var articlesDefaultPageString = await articlesDefaultPage.Content.ReadAsStringAsync();
            var articlesFirstPageString = await articlesFirstPage.Content.ReadAsStringAsync();
            var articlesSecondPageString = await articlesSecondPage.Content.ReadAsStringAsync();


            articlesDefaultPageString.ShouldNotBeNull();
            articlesFirstPageString.ShouldNotBeNull();
            articlesSecondPageString.ShouldNotBeNull();

            articlesDefaultPageString.ShouldContain(articlesFirstPageString);
            articlesDefaultPageString.ShouldNotContain(articlesSecondPageString);
        }

        [Fact]
        public async Task GetArticleDetails_Success()
        {
            var articlesFirstPage = await _client.GetAsync("/api/article/1");
            articlesFirstPage.EnsureSuccessStatusCode();
            var articlesFirstPageString = await articlesFirstPage.Content.ReadAsStringAsync();

            var firstArticle = JsonConvert.DeserializeObject<ArticlePagedListResponse>(articlesFirstPageString).Data.FirstOrDefault();

            var response = await _client.GetAsync("/api/article/" + firstArticle?.ArticleSlug);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ArticleDetailsVm>(responseString);

            Assert.IsType<ArticleDetailsVm>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PostArticle_Success()
        {
            var login = await AuthenticationHelper.LoginBasicUserAsync(_client);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Token);
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = Guid.Parse("9DDC7BE923AB47B3A2E9A9B2559FC87C"),
                ProviderId = Guid.Parse("7C87846121614769B2D181B7A7038D8F"),
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint", 
                ArticleSlug = "original-title"
            };

            var json = JsonConvert.SerializeObject(createArticleCommand);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/article", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateArticleCommandResponse>(responseString);

            Assert.IsType<CreateArticleCommandResponse>(result);
            Assert.NotNull(result?.Data.ArticleId);
        }

    }
}
