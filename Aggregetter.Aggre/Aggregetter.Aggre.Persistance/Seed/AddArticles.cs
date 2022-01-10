using Aggregetter.Aggre.Domain.Entities;
using Aggregetter.Aggre.Domain.Links;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Persistance.Seed
{
    public static class AddArticles
    {
        public static async Task InitiliseAsync(AggreDbContext context)
        {
            var currentTime = DateTime.UtcNow;

            var language = new Language()
            {
                Id = 1,
                Name = "Latin",
            };
            var languageResult = await context.Languages.SingleOrDefaultAsync(l => l.Id == language.Id);
            if (languageResult is null)
            {
                await context.Languages.AddAsync(language);
            }

            var category = new Category()
            {
                Id = 1,
                Name = "Old News",
            };
            var categoryResult = await context.Categories.SingleOrDefaultAsync(c => c.Id == category.Id);
            if (categoryResult is null)
            {
                await context.Categories.AddAsync(category);
            }

            var provider = new Provider()
            {
                Id = 1,
                LanguageId = language.Id,
                Name = "Latin news",
                BaseAddress = "lorem.example",
            };
            var providerResult = await context.Providers.SingleOrDefaultAsync(p => p.Id == provider.Id);
            if (providerResult is null)
            {
                await context.Providers.AddAsync(provider);
            }

            for (int i = 0; i < 100; i++)
            {
                var article = new Article
                {
                    Id = i + 1,
                    CategoryId = category.Id,
                    ProviderId = provider.Id,
                    OriginalBody = "Lorem Ipsum" + i,
                    TranslatedBody = "Dummy Text" + i,
                    OriginalTitle = "Lorem" + i,
                    TranslatedTitle = "Dummy" + i,
                    Endpoint = "Lorem/Endpoint" + i,
                    ArticleSlug = "lorem-ipsum-" + i
                };

                var articleResult = await context.Articles.SingleOrDefaultAsync(a => a.Endpoint == article.Endpoint);
                if (articleResult is null)
                {
                    await context.Articles.AddAsync(article);
                }

                var articleCategory = new ArticleCategory
                {
                    ArticleId = article.Id,
                    CategoryId = category.Id,
                };

                var articleCategoryResult = await context.ArticleCategories.SingleOrDefaultAsync(a => a.ArticleId == article.Id && a.CategoryId == category.Id);
                if (articleResult is null)
                {
                    await context.ArticleCategories.AddAsync(articleCategory);
                }
            }

            await context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
