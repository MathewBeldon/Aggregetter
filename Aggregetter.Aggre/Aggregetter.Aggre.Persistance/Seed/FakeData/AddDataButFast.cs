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

namespace Aggregetter.Aggre.Persistence.Seed
{
    public sealed class AddDataButFast
    {
        public static async Task InitiliseAsync(AggreDbContext context, ILogger logger)
        {
            var currentTime = DateTime.UtcNow;

            Random rnd = new();
            const int MinSeed = 10;

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
                    BaseAddress = new Uri($"https://lorem{i}.example"),
                };
                var providerResult = await context.Providers.SingleOrDefaultAsync(p => p.Id == provider.Id);
                if (providerResult is null)
                {
                    await context.Providers.AddAsync(provider);
                }
            }

            _ = await context.SaveChangesAsync(CancellationToken.None);

            var articles = new List<Article>();

            var translatedBody = $"There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which dont look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isnt anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.";
            
            const string query = $"INSERT INTO Articles (CategoryId,ProviderId,OriginalTitle,TranslatedTitle,OriginalBody,TranslatedBody,Endpoint,ArticleSlug,CreatedDateUtc,ModifiedDateUtc) VALUES";
            await context.Database.ExecuteSqlRawAsync("SET autocommit=0;SET unique_checks=0;SET foreign_key_checks=0;");
            var articleOffset = (await context.Articles.OrderByDescending(x => x.Id).FirstOrDefaultAsync())?.Id ?? 0;
            int batch = 0;
            int batchSize = 100;
            StringBuilder sb = new(query);
            Random random = new();

            for (int i = articleOffset; i < 1000; i++)
            { 
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
                var text = new string(Enumerable.Repeat(chars, (batchSize + 1) * 50 + 1000)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                var categoryId = rnd.Next(1, MinSeed);
                var providerId = rnd.Next(1, MinSeed);
                var x = batch * 50;
                var y = (batch + 1) * 1000;
                var originalTitle = text[(batch * 10)..(((batch + 1) * 10) - 1)] + i;
                var translatedTitle = $"What is Lorem Ipsum?{i}";
                var originalBody = text[(batch * 50)..((batch + 1) * 50 + 1000)] + i;
                var endpoint = $"Lorem/Endpoint{i}";
                var articleSlug = $"lorem-ipsum{i}";
                
                sb.Append($@"({categoryId},{providerId},'{originalTitle}','{translatedTitle}','{originalBody}','','{endpoint}','{articleSlug}',NOW(),'0001-01-01 00:00:00.000000')");
                if (++batch < batchSize)
                {
                    sb.Append(',');
                }
                else 
                {
                    sb.Append(";");
                    await context.Database.ExecuteSqlRawAsync(sb.ToString());
                    sb = new StringBuilder(query);
                    batch = 0;
                }
            }
            await context.Database.ExecuteSqlRawAsync("COMMIT;SET autocommit=1;SET unique_checks=1;SET foreign_key_checks=1;");
            context.Articles.AddRange(articles);
        }
    }
}
