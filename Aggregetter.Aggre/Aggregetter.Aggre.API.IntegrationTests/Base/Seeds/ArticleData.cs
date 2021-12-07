using Aggregetter.Aggre.Domain.Entities;
using Aggregetter.Aggre.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.IntegrationTests.Base.Seeds
{
    internal class ArticleData
    {
        internal async static Task<int> InitialiseAsync(AggreDbContext context)
        {
            var currentTime = DateTime.UtcNow;

            var language = new Language()
            {
                LanguageId = Guid.Parse("222569EDAC4041E6A4E9D73EC8B08483"),
                Name = "Latin",
            };
            var languageResult = await context.Languages.SingleOrDefaultAsync(x => x.LanguageId == language.LanguageId);
            if (languageResult is null)
            {
                await context.Languages.AddAsync(language);
            }

            var category = new Category()
            {
                CategoryId = Guid.Parse("9DDC7BE923AB47B3A2E9A9B2559FC87C"),
                Name = "Old News",
            };
            var categoryResult = await context.Categories.SingleOrDefaultAsync(x => x.CategoryId == category.CategoryId);
            if (categoryResult is null)
            {
                await context.Categories.AddAsync(category);
            }

            var provider = new Provider()
            {
                ProviderId = Guid.Parse("7C87846121614769B2D181B7A7038D8F"),
                LanguageId = language.LanguageId,
                Name = "Roman news",
                BaseAddress = "google.com",
            };
            var providerResult = await context.Providers.SingleOrDefaultAsync(x => x.ProviderId == provider.ProviderId);
            if (providerResult is null)
            {
                await context.Providers.AddAsync(provider);
            }

            for (int i = 0; i < 100; i++)
            {
                var article = new Article
                {
                    ArticleId = Guid.NewGuid(),
                    CategoryId = category.CategoryId,
                    ProviderId = provider.ProviderId,
                    OriginalBody = "Lorem Ipsum" + i,
                    TranslatedBody = "Dummy Text" + i,
                    OriginalTitle = "Lorem" + i,
                    TranslatedTitle = "Dummy" +  i,
                    Endpoint = "Lorem/Endpoint" + i,
                    ArticleSlug = "lorem-ipsum" + i
                };

                var articleResult = await context.Articles.SingleOrDefaultAsync(x => x.ArticleId == article.ArticleId);
                if (articleResult is null)
                {
                    await context.Articles.AddAsync(article);
                }
            }
            

            return await context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
