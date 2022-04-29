using Aggregetter.Aggre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Persistence.Seed
{
    public static class AddArticles
    {
        public static async Task InitiliseAsync(AggreDbContext context, ILogger logger)
        {
            var currentTime = DateTime.UtcNow;

            Random rnd = new();
            const int MinSeed = 100;

            var languageOffset = await context.Languages.CountAsync();
            for (int i = languageOffset; i < MinSeed; i++)
            {
                var language = new Language()
                {
                    Id = i + 1,
                    Name = $"Latin{i}",
                };
                var languageResult = await context.Languages.SingleOrDefaultAsync(l => l.Id == language.Id);
                if (languageResult is null)
                {
                    await context.Languages.AddAsync(language);
                }
            }

            var categoryOffset = await context.Categories.CountAsync();
            for (int i = categoryOffset; i < MinSeed; i++)
            {
                var category = new Category()
                {
                    Id = i + 1,
                    Name = $"Old News{i}",
                };
                var categoryResult = await context.Categories.SingleOrDefaultAsync(c => c.Id == category.Id);
                if (categoryResult is null)
                {
                    await context.Categories.AddAsync(category);
                }
            }

            var providerOffset = await context.Providers.CountAsync();
            for (int i = categoryOffset; i < MinSeed; i++)
            {
                var provider = new Provider()
                {
                    Id = i + 1,
                    LanguageId = rnd.Next(1, MinSeed),
                    Name = $"Latin news{i}",
                    BaseAddress = new Uri($"lorem{i}.example"),
                };
                var providerResult = await context.Providers.SingleOrDefaultAsync(p => p.Id == provider.Id);
                if (providerResult is null)
                {
                    await context.Providers.AddAsync(provider);
                }
            }

            var articles = new List<Article>();

            var articleOffset = (await context.Articles.OrderByDescending(x => x.Id).FirstOrDefaultAsync()).Id;
            int batch = 0;
            for (int i = articleOffset; i < 15000000; i++)
            {
                var article = new Article
                {
                    Id = i + 1,
                    CategoryId = rnd.Next(1, MinSeed),
                    ProviderId = rnd.Next(1, MinSeed),
                    OriginalBody = $"Съществуват много вариации на пасажа Lorem Ipsum, но повечето от тях са променени по един или друг начин чрез добавяне на смешни думи или разбъркване на думите, което не изглежда много достоверно. Ако искате да използвате пасаж от Lorem Ipsum, трябва да сте сигурни, че в него няма смущаващи или нецензурни думи. Всички Lorem Ipsum генератори в Интернет използват предефинирани пасажи, който се повтарят, което прави този този генератор първия истински такъв. Той използва речник от над 200 латински думи, комбинирани по подходящ начин като изречения, за да генерират истински Lorem Ipsum пасажи. Оттук следва, че генерираният Lorem Ipsum пасаж не съдържа повторения, смущаващи, нецензурни и всякакви неподходящи думи. {i}",
                    TranslatedBody = $"There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc. {i}",
                    OriginalTitle = $"Какво е Lorem Ipsum? {i}",
                    TranslatedTitle = $"What is Lorem Ipsum? {i}",
                    Endpoint = $"Lorem/Endpoint{i}",
                    ArticleSlug = $"lorem-ipsum{i}"
                };
                articles.Add(article);

                if (batch++ > 10000)
                {
                    logger.Information("Adding 10000 records");
                    batch = 0;
                    context.Articles.AddRange(articles);
                    context.ChangeTracker.DetectChanges();
                    await context.SaveChangesAsync(CancellationToken.None);
                    articles.Clear();
                    logger.Information($"Added 10000 records, total {i}");
                }
            }
            context.Articles.AddRange(articles);

            await context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
