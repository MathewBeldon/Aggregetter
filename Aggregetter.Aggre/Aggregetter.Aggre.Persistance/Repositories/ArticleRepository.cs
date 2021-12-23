using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Persistance.Repositories
{
    public sealed class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(AggreDbContext context, IDistributedCache cache) : base (context, cache)
        {
        }

        public async Task<bool> IsArticleEndpointUniqueAsync(string endpoint, CancellationToken cancellationToken)
        {
            return !await _context.Articles.AnyAsync(article => article.Endpoint == endpoint, cancellationToken);
        }

        public async Task<Article> GetArticleBySlugAsync(string articleSlug, CancellationToken cancellationToken)
        {
            var queryEncoded = await _cache.GetAsync(articleSlug, cancellationToken);

            if (queryEncoded is null)
            {
                var entity = await _context.Articles.SingleOrDefaultAsync(article => article.ArticleSlug == articleSlug, cancellationToken);

                _ = Task.Run(() => CacheObject(articleSlug, EncodeObject(entity), cancellationToken));

                return entity;
            }

            return JsonConvert.DeserializeObject<Article>(Encoding.UTF8.GetString(queryEncoded));
        }
    }
}
