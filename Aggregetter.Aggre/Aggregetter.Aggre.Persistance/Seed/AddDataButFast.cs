using Aggregetter.Aggre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Persistance.Seed
{
    public sealed class AddDataButFast
    {
        public static async Task InitiliseAsync(AggreDbContext context, ILogger logger)
        {
            var currentTime = DateTime.UtcNow;

            Random rnd = new Random();
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
                    BaseAddress = $"lorem{i}.example",
                };
                var providerResult = await context.Providers.SingleOrDefaultAsync(p => p.Id == provider.Id);
                if (providerResult is null)
                {
                    await context.Providers.AddAsync(provider);
                }
            }

            var articles = new List<Article>();

            var originalBody = $"Съществуват много вариации на пасажа Lorem Ipsum, но повечето от тях са променени по един или друг начин чрез добавяне на смешни думи или разбъркване на думите, което не изглежда много достоверно. Ако искате да използвате пасаж от Lorem Ipsum, трябва да сте сигурни, че в него няма смущаващи или нецензурни думи. Всички Lorem Ipsum генератори в Интернет използват предефинирани пасажи, който се повтарят, което прави този този генератор първия истински такъв. Той използва речник от над 200 латински думи, комбинирани по подходящ начин като изречения, за да генерират истински Lorem Ipsum пасажи. Оттук следва, че генерираният Lorem Ipsum пасаж не съдържа повторения, смущаващи, нецензурни и всякакви неподходящи думи.";
            var translatedBody = $"There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which dont look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isnt anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.";
            
            
            const string query = $"SET autocommit=0;SET unique_checks=0;SET foreign_key_checks=0;INSERT INTO articles (CategoryId,ProviderId,OriginalTitle,TranslatedTitle,OriginalBody,TranslatedBody,Endpoint,ArticleSlug,CreatedDateUtc,ModifiedDateUtc) VALUES";

            var articleOffset = (await context.Articles.OrderByDescending(x => x.Id).FirstOrDefaultAsync()).Id;
            int batch = 0;
            StringBuilder sb = new StringBuilder(query);
            for (int i = articleOffset; i < 20000000; i++)
            {
                var categoryId = rnd.Next(1, MinSeed);
                var providerId = rnd.Next(1, MinSeed);
                var originalTitle = $"Какво е Lorem Ipsum?{i}";
                var translatedTitle = $"What is Lorem Ipsum?{i}";
                var endpoint = $"Lorem/Endpoint{i}";
                var articleSlug = $"lorem-ipsum{i}";

                sb.Append($@"({categoryId},{providerId},'{originalTitle}','{translatedTitle}','{originalBody}','{translatedBody}','{endpoint}','{articleSlug}',NOW(),'0001-01-01 00:00:00.000000')");
                if (++batch < 5000)
                {
                    sb.Append(",");
                }
                else 
                {
                    sb.Append(";COMMIT;SET unique_checks=1;SET foreign_key_checks=1;");
                    logger.Information("writing 500 records");
                    await context.Database.ExecuteSqlRawAsync(sb.ToString());
                    logger.Information($"written 500 records, total {i}");
                    sb = new StringBuilder(query);
                    batch = 0;
                }
            }
            context.Articles.AddRange(articles);

        }
    }
}
