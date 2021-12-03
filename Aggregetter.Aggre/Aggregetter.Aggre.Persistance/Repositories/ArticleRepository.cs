using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        public ArticleRepository(AggreDbContext context, IDistributedCache cache, IConfiguration configuration, ILogger<BaseRepository<Article>> logger) : base (context, cache, configuration, logger)
        {
        }

        public async Task<bool> IsArticleEndpointUniqueAsync(string endpoint, CancellationToken cancellationToken)
        {
            return !await _context.Articles.AnyAsync(article => article.Endpoint == endpoint, cancellationToken);
        }
    }
}
