using Aggregetter.Aggre.API.IntegrationTests.Base;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles.Base;
using Aggregetter.Aggre.Application.Models.Base;
using FluentValidation.Results;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Aggregetter.Aggre.API.IntegrationTests.Controllers
{
    public sealed class ArticleControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public ArticleControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
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
            var articlesResult = JsonConvert.DeserializeObject<GetArticlesQueryResponse>(articlesFirstPageString) ?? throw new ArgumentNullException();

            articlesFirstPageString.ShouldNotBeNull();
            articlesSecondPageString.ShouldNotBeNull();
            articlesFirstPageString.ShouldNotContain(articlesSecondPageString);
            articlesResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task GetArticlesByPage_v1_InvalidPage_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles?pagesize=20&page=0");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();
            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.Success.ShouldBeFalse();
            articlesResult.ValidationErrors.Count.ShouldBe(1);
        }

        [Fact]
        public async Task GetArticlesByPage_v1_InvalidPageSize_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles?pagesize=200&page=1");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();
            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.Success.ShouldBeFalse();
            articlesResult.ValidationErrors.Count.ShouldBe(1);
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

            var articlesResult = JsonConvert.DeserializeObject<GetArticlesQueryResponse>(articlesFirstPageString) ?? throw new ArgumentNullException();

            articlesFirstPageString.ShouldNotBeNull();
            articlesSecondPageString.ShouldNotBeNull();
            articlesFirstPageString.ShouldNotContain(articlesSecondPageString);
            articlesResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task GetArticlesByPageByCategory_v1_InvalidPage_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/category/1?pagesize=20&page=0");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.Success.ShouldBeFalse();
            articlesResult.ValidationErrors.Count.ShouldBe(1);
        }


        [Fact]
        public async Task GetArticlesByPageByCategory_v1_InvalidPageSize_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/category/1?pagesize=200&page=1");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.Success.ShouldBeFalse();
            articlesResult.ValidationErrors.Count.ShouldBe(1);
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

            var articlesResult = JsonConvert.DeserializeObject<GetArticlesQueryResponse>(articlesFirstPageString) ?? throw new ArgumentNullException();

            articlesFirstPageString.ShouldNotBeNull();
            articlesSecondPageString.ShouldNotBeNull();
            articlesFirstPageString.ShouldNotContain(articlesSecondPageString);
            articlesResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task GetArticlesByPageByProvider_v1_InvalidPage_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/provider/1?pagesize=20&page=0");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.Success.ShouldBeFalse();
            articlesResult.ValidationErrors.Count.ShouldBe(1);
        }


        [Fact]
        public async Task GetArticlesByPageByProvider_v1_InvalidPageSize_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/provider/1?pagesize=200&page=1");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.Success.ShouldBeFalse();
            articlesResult.ValidationErrors.Count.ShouldBe(1);
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

            var articlesResult = JsonConvert.DeserializeObject<GetArticlesQueryResponse>(articlesFirstPageString) ?? throw new ArgumentNullException();

            articlesFirstPageString.ShouldNotBeNull();
            articlesSecondPageString.ShouldNotBeNull();
            articlesFirstPageString.ShouldNotContain(articlesSecondPageString);
            articlesResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task GetArticlesByPageByProviderAndCategory_v1_InvalidPage_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/provider/1/category/1?pagesize=20&page=0");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.Success.ShouldBeFalse();
            articlesResult.ValidationErrors.Count.ShouldBe(1);
        }


        [Fact]
        public async Task GetArticlesByPageByProviderAndCategory_v1_InvalidPageSize_ValidationError()
        {
            var articlesPage = await _client.GetAsync("/api/v1/articles/provider/1/category/1?pagesize=200&page=1");

            var articlesPageString = await articlesPage.Content.ReadAsStringAsync();

            var articlesResult = JsonConvert.DeserializeObject<BaseResponse>(articlesPageString) ?? throw new ArgumentNullException();

            articlesResult.Success.ShouldBeFalse();
            articlesResult.ValidationErrors.Count.ShouldBe(1);
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

            result.ShouldBeOfType<GetArticleDetailsQueryResponse>();
            result.Success.ShouldBeTrue();
            result.ShouldNotBeNull();
        }

        #endregion GetArticleDetails

        #region PostArticle

        [Fact]
        public async Task PostArticle_v1_ValidRequest_Success()
        {
            var login = await AuthenticationHelper.LoginBasicUserAsync(_client, version: 1);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Token);
            var createArticleCommand = new CreateArticleCommand()
            {
                CategoryId = 1,
                ProviderId = 1,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
                Endpoint = "New/Endpoint", 
                ArticleSlug = "original-title"
            };

            var json = JsonConvert.SerializeObject(createArticleCommand);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/articles", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateArticleCommandResponse>(responseString);

            result.ShouldBeOfType<CreateArticleCommandResponse>();
            result.Data.Id.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task PostArticle_v1_ValidationError_DuplicateEndpoint()
        {
            var login = await AuthenticationHelper.LoginBasicUserAsync(_client, version: 1);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Token);

            var firstArticleCommand = new CreateArticleCommand()
            {
                CategoryId = 1,
                ProviderId = 1,
                OriginalTitle = "Original Title",
                TranslatedTitle = "Translated Title",
                OriginalBody = "Original Body",
                TranslatedBody = "Translated Body",
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
                Endpoint = "Taken/Endpoint",
                ArticleSlug = "taken2-title"
            };
            var duplicateEndpointArticleJson = JsonConvert.SerializeObject(duplicateEndpointArticleCommand);
            var duplicateEndpointArticleContent = new StringContent(duplicateEndpointArticleJson, Encoding.UTF8, "application/json");
            var duplictePost = await _client.PostAsync("/api/v1/articles", duplicateEndpointArticleContent);
            var duplicatePostString = await duplictePost.Content.ReadAsStringAsync();
            var duplicatePostResult = JsonConvert.DeserializeObject<BaseResponse>(duplicatePostString) ?? throw new ArgumentNullException();

            duplicatePostResult.Success.ShouldBeFalse();
            duplicatePostResult.ValidationErrors.Count.ShouldBe(1);
        }

        #endregion PostArticle
    }
}
