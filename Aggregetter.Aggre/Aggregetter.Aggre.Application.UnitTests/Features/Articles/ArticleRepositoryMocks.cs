﻿using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Articles
{
    public sealed class ArticleRepositoryMocks
    {
        public static Mock<IArticleRepository> GetArticleRepository()
        {
            List<Article> articles = new List<Article>();

            for (int i = 0; i < 100; i++)
            {
                var date = DateTime.UtcNow;
                articles.Add(new Article
                {
                    Id = i + 1,
                    CategoryId = 1,
                    ProviderId = 1,
                    OriginalTitle = "Lorem" + i,
                    TranslatedTitle = "Dummy" + i,
                    OriginalBody = "Lorem Ipsum" + i,
                    TranslatedBody = "Dummy Text" + i,
                    CreatedDateUtc = date,
                    ModifiedDateUtc = date,
                    Endpoint = "Taken/Endpoint" + i,
                    ArticleSlug = "dummy-text" + i
                });
            }

            var mockArticleRepository = new Mock<IArticleRepository>();

            mockArticleRepository.Setup(repo => repo.GetCount(CancellationToken.None)).ReturnsAsync(
                (CancellationToken cancellationToken) =>
                {
                    return articles.Count;
                });

            mockArticleRepository.Setup(repo => repo.GetCountByCategory(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(
                (int categoryId, CancellationToken cancellationToken) =>
                {
                    return articles.Where(a => a.CategoryId == categoryId).Count();
                });

            mockArticleRepository.Setup(repo => repo.GetArticleBySlugAsync(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(
                (string articleSlug, CancellationToken cancellationToken) =>
                {
                    return articles.FirstOrDefault(a => a.ArticleSlug == articleSlug);
                });

            
            mockArticleRepository.Setup(repo => repo.ArticleEndpointExistsAsync(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(
                (string endpoint, CancellationToken cancellationToken) =>
                {
                    if (articles.Any(article => article.Endpoint == endpoint))
                    {
                        return true;
                    }
                    return false;
                });

            mockArticleRepository.Setup(repo => repo.ArticleSlugExistsAsync(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(
                (string articleSlug, CancellationToken cancellationToken) =>
                {
                    if (articles.Any(article => article.ArticleSlug == articleSlug))
                    {
                        return true;
                    }
                    return false;
                });

            mockArticleRepository.Setup(repo => repo.AddAsync(It.IsAny<Article>(), CancellationToken.None)).ReturnsAsync(
                (Article article, CancellationToken cancellationToken) =>
                {
                    articles.Add(article);
                    return article;
                });

            mockArticleRepository.Setup(repo => repo.GetArticlesPagedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(),  It.IsAny<CancellationToken>())).ReturnsAsync(
                (int page, int pageSize, int totalCount, CancellationToken cancellationToken) =>
                {
                    var articleList = articles.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    foreach(var article in articleList)
                    {
                        article.Category = new Category()
                        {
                            Id = article.CategoryId,
                            Name = "CategoryName",
                            CreatedDateUtc = DateTime.UtcNow,
                        };

                        article.Provider = new Provider()
                        {
                            Id = article.ProviderId,
                            Name = "ProviderName",
                            BaseAddress = new Uri("https://base.address.example"),
                            CreatedDateUtc = DateTime.UtcNow,
                            Language = new Language()
                        };
                    }

                    return articleList;
                });

            mockArticleRepository.Setup(repo => repo.GetArticlesByCategoryPagedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(
                (int page, int pageSize, int categoryId, CancellationToken cancellationToken) =>
                {
                    var articleList = articles.Where(x => x.CategoryId == categoryId).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    foreach (var article in articleList)
                    {
                        article.Category = new Category()
                        {
                            Id = article.CategoryId,
                            Name = "CategoryName",
                            CreatedDateUtc = DateTime.UtcNow,
                        };

                        article.Provider = new Provider()
                        {
                            Id = article.ProviderId,
                            Name = "ProviderName",
                            BaseAddress = new Uri("https://base.address.example"),
                            CreatedDateUtc = DateTime.UtcNow,
                        };
                    }

                    return articleList;
                });

            mockArticleRepository.Setup(repo => repo.GetArticlesByProviderPagedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(
                (int page, int pageSize, int providerId, CancellationToken cancellationToken) =>
                {
                    var articleList = articles.Where(x => x.ProviderId == providerId).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    foreach (var article in articleList)
                    {
                        article.Category = new Category()
                        {
                            Id = article.CategoryId,
                            Name = "CategoryName",
                            CreatedDateUtc = DateTime.UtcNow,
                        };

                        article.Provider = new Provider()
                        {
                            Id = article.ProviderId,
                            Name = "ProviderName",
                            BaseAddress = new Uri("https://base.address.example"),
                            CreatedDateUtc = DateTime.UtcNow,
                        };
                    }

                    return articleList;
                });

            mockArticleRepository.Setup(repo => repo.GetArticlesByProviderAndCategoryPagedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(
                (int page, int pageSize, int providerId, int categoryId, CancellationToken cancellationToken) =>
                {
                    var articleList = articles.Where(x => x.ProviderId == providerId && x.CategoryId == categoryId)
                        .Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    foreach (var article in articleList)
                    {
                        article.Category = new Category()
                        {
                            Id = article.CategoryId,
                            Name = "CategoryName",
                            CreatedDateUtc = DateTime.UtcNow,
                        };

                        article.Provider = new Provider()
                        {
                            Id = article.ProviderId,
                            Name = "ProviderName",
                            BaseAddress = new Uri("https://base.address.example"),
                            CreatedDateUtc = DateTime.UtcNow,
                        };
                    }

                    return articleList;
                });

            return mockArticleRepository;
        }
    }
}
