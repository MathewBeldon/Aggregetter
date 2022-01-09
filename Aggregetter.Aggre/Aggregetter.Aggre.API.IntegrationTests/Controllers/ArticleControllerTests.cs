﻿using Aggregetter.Aggre.API.IntegrationTests.Base;
using Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticleDetails;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles;
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
    public sealed class ArticleControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
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
            var articlesFirstPage = await _client.GetAsync("/api/article?pagesize=20&page=1");
            articlesFirstPage.EnsureSuccessStatusCode();

            var articlesSecondPage = await _client.GetAsync("/api/article?pagesize=20&page=2");
            articlesSecondPage.EnsureSuccessStatusCode();

            var articlesFirstPageString = await articlesFirstPage.Content.ReadAsStringAsync();
            var articlesSecondPageString = await articlesSecondPage.Content.ReadAsStringAsync();


            articlesFirstPageString.ShouldNotBeNull();
            articlesSecondPageString.ShouldNotBeNull();
            articlesFirstPageString.ShouldNotContain(articlesSecondPageString);
        }

        [Fact]
        public async Task GetArticleDetails_Success()
        {
            var articlesFirstPage = await _client.GetAsync("/api/article?pagesize=20&page=1");
            articlesFirstPage.EnsureSuccessStatusCode();
            var articlesFirstPageString = await articlesFirstPage.Content.ReadAsStringAsync();

            var firstArticle = JsonConvert.DeserializeObject<GetArticlesQueryResponse>(articlesFirstPageString).Data.FirstOrDefault();

            var response = await _client.GetAsync("/api/article/" + firstArticle?.ArticleSlug);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GetArticleDetailsQueryResponse>(responseString);

            Assert.IsType<GetArticleDetailsQueryResponse>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PostArticle_Success()
        {
            var login = await AuthenticationHelper.LoginBasicUserAsync(_client);
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

            var response = await _client.PostAsync("/api/article", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateArticleCommandResponse>(responseString);

            Assert.IsType<CreateArticleCommandResponse>(result);
            Assert.NotNull(result?.Data.ArticleId);
        }

        [Fact]
        public async Task PostArticle_ValidationError_DuplicateEndpoint()
        {
            var login = await AuthenticationHelper.LoginBasicUserAsync(_client);
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
            var firstPost = await _client.PostAsync("/api/article", firstArticleContent);
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
            var duplictePost = await _client.PostAsync("/api/article", duplicateEndpointArticleContent);
            duplictePost.StatusCode.ShouldBe(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
