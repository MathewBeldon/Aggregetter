using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Aggregetter.Aggre.Application.UnitTests.Articles
{
    public class ArticleRepositoryMocks
    {
        public static Mock<IArticleRepository> GetArticleRepository()
        {
            var languageId = Guid.Parse("6AB570BD015E48F1A522F9067168C92B");
            var categoryId = Guid.Parse("0763EBF37CC443A3B3AFD7F94109934C");
            var providerId = Guid.Parse("03867E6157024CBF9403716F4F900519");

            var articles = new List<Article>();

            for (int i = 0; i < 100; i++)
            {
                articles.Add(new Article
                {
                    ArticleId = Guid.NewGuid(),
                    CategoryId = categoryId,
                    ProviderId = providerId,
                    OriginalTitle = "Lorem" + i,
                    TranslatedTitle = "Dummy" + i,
                    OriginalBody = "Lorem Ipsum" + i,
                    TranslatedBody = "Dummy Text" + i,
                    CreatedDateUtc = DateTime.UtcNow,
                    ModifiedDateUtc = DateTime.UtcNow,
                    Endpoint = "Taken/Endpoint" + i,
                    ArticleSlug = "dummy-text" + i
                });
            }

            var mockArticleRepository = new Mock<IArticleRepository>();

            mockArticleRepository.Setup(repo => repo.GetAllAsync(CancellationToken.None)).ReturnsAsync(articles);

            mockArticleRepository.Setup(repo => repo.IsArticleEndpointUniqueAsync(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(
                (string endpoint, CancellationToken cancellationToken) =>
                {
                    if (!articles.Any(article => article.Endpoint == endpoint))
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

            mockArticleRepository.Setup(repo => repo.GetPagedResponseAsync(It.IsAny<int>(), It.IsAny<int>(),  It.IsAny<CancellationToken>())).ReturnsAsync(
                (int page, int pageSize, CancellationToken cancellationToken) =>
                {
                    return articles.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                });

            return mockArticleRepository;
        }
    }
}
