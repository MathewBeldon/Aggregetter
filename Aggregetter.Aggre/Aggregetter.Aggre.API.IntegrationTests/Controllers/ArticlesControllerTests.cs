using Aggregetter.Aggre.API.IntegrationTests.Base;
using Aggregetter.Aggre.API.IntegrationTests.Base.Helpers;
using Aggregetter.Aggre.API.IntegrationTests.Base.Seeds;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Models.Base;
using Aggregetter.Aggre.Persistence;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.API.IntegrationTests.Controllers
{
    public sealed class ArticlesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    { 
        private readonly HttpClient _client;

        public ArticlesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.Client;
        }

        #region GetArticlesByPage

        [Fact]
        public async Task GetArticlesByPage_v1_ValidRequest_Success()
        {
            var articlesFirstPage = await _client.GetAsync("/api/v1/articles?pagesize=20&page=1");
            articlesFirstPage.EnsureSuccessStatusCode();
            var articlesSecondPage = await _client.GetAsync("/api/v1/articles?pagesize=20&page=2");
            articlesSecondPage.EnsureSuccessStatusCode();

            var articlesFirstPageString = await articlesFirstPage.Content.ReadAsStringAsync();
            var articlesSecondPageString = await articlesSecondPage.Content.ReadAsStringAsync();

            articlesFirstPageString.Should().NotBeNull();
            articlesSecondPageString.Should().NotBeNull();
            articlesFirstPageString.Should().NotContain(articlesSecondPageString);
        }

        [Fact]
        public async Task GetArticlesByPage_v1_InvalidPage_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles?pagesize=20&page=0");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();
            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.ValidationErrors.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetArticlesByPage_v1_InvalidPageSize_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles?pagesize=200&page=1");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();
            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.ValidationErrors.Count.Should().Be(1);
        }

        #endregion GetArticlesByPage

        #region GetArticlesByPageByCategory

        [Fact]
        public async Task GetArticlesByPageByCategory_v1_ValidRequest_Success()
        {
            var articlesFirstPage = await _client.GetAsync("/api/v1/articles/category/1?pagesize=20&page=1");
            articlesFirstPage.EnsureSuccessStatusCode();

            var articlesSecondPage = await _client.GetAsync("/api/v1/articles/category/1?pagesize=20&page=2");
            articlesSecondPage.EnsureSuccessStatusCode();

            var articlesFirstPageString = await articlesFirstPage.Content.ReadAsStringAsync();
            var articlesSecondPageString = await articlesSecondPage.Content.ReadAsStringAsync();

            articlesFirstPageString.Should().NotBeNull();
            articlesSecondPageString.Should().NotBeNull();
            articlesFirstPageString.Should().NotContain(articlesSecondPageString);
        }

        [Fact]
        public async Task GetArticlesByPageByCategory_v1_InvalidPage_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/category/1?pagesize=20&page=0");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.ValidationErrors.Count.Should().Be(1);
        }


        [Fact]
        public async Task GetArticlesByPageByCategory_v1_InvalidPageSize_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/category/1?pagesize=200&page=1");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.ValidationErrors.Count.Should().Be(1);
        }

        #endregion GetArticlesByPageByCategory

        #region GetArticlesByPageByProvider

        [Fact]
        public async Task GetArticlesByPageByProvider_v1_ValidRequest_Success()
        {
            var articlesFirstPage = await _client.GetAsync("/api/v1/articles/provider/1?pagesize=20&page=1");
            articlesFirstPage.EnsureSuccessStatusCode();

            var articlesSecondPage = await _client.GetAsync("/api/v1/articles/provider/1?pagesize=20&page=2&providerId=1");
            articlesSecondPage.EnsureSuccessStatusCode();

            var articlesFirstPageString = await articlesFirstPage.Content.ReadAsStringAsync();
            var articlesSecondPageString = await articlesSecondPage.Content.ReadAsStringAsync();

            articlesFirstPageString.Should().NotBeNull();
            articlesSecondPageString.Should().NotBeNull();
            articlesFirstPageString.Should().NotContain(articlesSecondPageString);
        }

        [Fact]
        public async Task GetArticlesByPageByProvider_v1_InvalidPage_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/provider/1?pagesize=20&page=0");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.ValidationErrors.Count.Should().Be(1);
        }


        [Fact]
        public async Task GetArticlesByPageByProvider_v1_InvalidPageSize_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/provider/1?pagesize=200&page=1");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.ValidationErrors.Count.Should().Be(1);
        }

        #endregion GetArticlesByPageByProvider

        #region GetArticlesByPageByProviderAndCategory

        [Fact]
        public async Task GetArticlesByPageByProviderAndCategory_v1_ValidRequest_Success()
        {
            var articlesFirstPage = await _client.GetAsync("/api/v1/articles/provider/1/category/1?pagesize=20&page=1");
            articlesFirstPage.EnsureSuccessStatusCode();

            var articlesSecondPage = await _client.GetAsync("/api/v1/articles/provider/1/category/1?pagesize=20&page=2");
            articlesSecondPage.EnsureSuccessStatusCode();

            var articlesFirstPageString = await articlesFirstPage.Content.ReadAsStringAsync();
            var articlesSecondPageString = await articlesSecondPage.Content.ReadAsStringAsync();

            articlesFirstPageString.Should().NotBeNull();
            articlesSecondPageString.Should().NotBeNull();
            articlesFirstPageString.Should().NotContain(articlesSecondPageString);
        }

        [Fact]
        public async Task GetArticlesByPageByProviderAndCategory_v1_InvalidPage_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/provider/1/category/1?pagesize=20&page=0");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.ValidationErrors.Count.Should().Be(1);
        }


        [Fact]
        public async Task GetArticlesByPageByProviderAndCategory_v1_InvalidPageSize_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/provider/1/category/1?pagesize=200&page=1");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.ValidationErrors.Count.Should().Be(1);
        }

        #endregion GetArticlesByPageByProviderAndCategory

        #region GetArticleDetails

        [Fact]
        public async Task GetArticleDetails_v1_ValidRequest_Success()
        {
            var articlesFirstPage = await _client.GetAsync("/api/v1/articles?pagesize=20&page=1");
            articlesFirstPage.EnsureSuccessStatusCode();
            var articlesFirstPageString = await articlesFirstPage.Content.ReadAsStringAsync();

            var firstArticle = JsonConvert.DeserializeObject<GetArticlesQueryResponse>(articlesFirstPageString)?.Data.FirstOrDefault();

            var response = await _client.GetAsync("/api/v1/articles/" + firstArticle?.ArticleSlug);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GetArticleDetailsQueryResponse>(responseString);

            result.Should().BeOfType<GetArticleDetailsQueryResponse>();
            result.Should().NotBeNull();
        }

        #endregion GetArticleDetails

        #region PostArticle

        [Fact]
        public async Task PostArticle_v1_ValidRequest_Success()
        {
            var login = await AuthenticationHelper.LoginBasicUserAsync(_client, version: 1);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Data.Token);
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = 1,
                ProviderId = 1,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                TranslatedBy = "Known@Email.com",
                Endpoint = "New/Endpoint", 
                ArticleSlug = "original-title"
            };

            var json = JsonConvert.SerializeObject(createArticleCommand);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/articles", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateArticleCommandResponse>(responseString);

            result.Should().BeOfType<CreateArticleCommandResponse>();
            result.Data.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task PostArticle_v1_ValidationError_DuplicateEndpoint()
        {
            var login = await AuthenticationHelper.LoginBasicUserAsync(_client, version: 1);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Data.Token);

            var firstArticleCommand = new CreateArticleCommand()
            {
                CategoryId = 1,
                ProviderId = 1,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                TranslatedBy = "Known@Email.com",
                Endpoint = "Taken/Endpoint",
                ArticleSlug = "taken-title"
            };            
            var firstArticleJson = JsonConvert.SerializeObject(firstArticleCommand);
            var firstArticleContent = new StringContent(firstArticleJson, Encoding.UTF8, "application/json");
            var firstPost = await _client.PostAsync("/api/v1/articles", firstArticleContent);
            firstPost.EnsureSuccessStatusCode();

            var duplicateEndpointArticleCommand = new CreateArticleCommand()
            {
                CategoryId = 1,
                ProviderId = 1,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                TranslatedBy = "Known@Email.com",
                Endpoint = "Taken/Endpoint",
                ArticleSlug = "taken2-title"
            };
            var duplicateEndpointArticleJson = JsonConvert.SerializeObject(duplicateEndpointArticleCommand);
            var duplicateEndpointArticleContent = new StringContent(duplicateEndpointArticleJson, Encoding.UTF8, "application/json");
            var duplictePost = await _client.PostAsync("/api/v1/articles", duplicateEndpointArticleContent);
            var duplicatePostString = await duplictePost.Content.ReadAsStringAsync();
            var duplicatePostResult = JsonConvert.DeserializeObject<BaseResponse>(duplicatePostString) ?? throw new ArgumentNullException();

            duplicatePostResult.ValidationErrors.Count.Should().Be(1);
        }

        #endregion PostArticle
    }
}
