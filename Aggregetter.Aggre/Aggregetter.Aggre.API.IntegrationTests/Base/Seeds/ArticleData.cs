using Aggregetter.Aggre.Domain.Entities;
using Aggregetter.Aggre.Persistence;
using System;
using System.Linq;

namespace Aggregetter.Aggre.API.IntegrationTests.Base.Seeds
{
    internal class ArticleData
    {
        internal static void Initialise(AggreDbContext context)
        {
            var currentTime = DateTime.UtcNow;

            var language = new Language()
            {
                Id = 1,
                Name = "Latin",
            };
            var languageResult = context.Languages.SingleOrDefault(l => l.Id == language.Id);
            if (languageResult is null)
            {
                context.Languages.Add(language);
            }

            var category = new Category()
            {
                Id = 1,
                Name = "Old News",
            };
            var categoryResult = context.Categories.SingleOrDefault(c => c.Id == category.Id);
            if (categoryResult is null)
            {
                context.Categories.Add(category);
            }

            var provider = new Provider()
            {
                Id = 1,
                LanguageId = language.Id,
                Name = "Roman news",
                BaseAddress = new Uri("https://base.address.example"),
            };
            var providerResult = context.Providers.SingleOrDefault(p => p.Id == provider.Id);
            if (providerResult is null)
            {
                context.Providers.Add(provider);
            }

            for (int i = 0; i < 100; i++)
            {
                var article = new Article
                {
                    CategoryId = category.Id,
                    ProviderId = provider.Id,
                    OriginalBody = "Lorem Ipsum" + i,
                    TranslatedBody = "Dummy Text" + i,
                    OriginalTitle = "Lorem" + i,
                    TranslatedTitle = "Dummy" +  i,
                    TranslatedBy = "user@email.com",
                    Endpoint = "Lorem/Endpoint" + i,
                    ArticleSlug = "lorem-ipsum" + i
                };

                var articleResult = context.Articles.SingleOrDefault(a => a.Id == article.Id);
                if (articleResult is null)
                {
                    context.Articles.Add(article);
                }
            }            

            context.SaveChanges();
        }
    }
}
