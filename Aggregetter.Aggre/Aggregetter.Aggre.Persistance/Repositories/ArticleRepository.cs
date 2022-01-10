using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles;
using Aggregetter.Aggre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Persistance.Repositories
{
    public sealed class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(AggreDbContext context) : base (context)
        {
        }

        public async Task<bool> ArticleEndpointExistsAsync(string endpoint, CancellationToken cancellationToken)
        {
            return await _context.Articles.AnyAsync(article => article.Endpoint == endpoint, cancellationToken);
        }

        public async Task<bool> ArticleSlugExistsAsync(string articleSlug, CancellationToken cancellationToken)
        {
            return await _context.Articles.AnyAsync(article => article.ArticleSlug == articleSlug, cancellationToken);
        }

        public async Task<Article> GetArticleBySlugAsync(string articleSlug, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles.SingleOrDefaultAsync(article => article.ArticleSlug == articleSlug, cancellationToken);

            return entity;            
        }

        public async Task<List<Article>> GetArticlesByPageAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles
                .OrderByDescending(x => x.CreatedDateUtc)
                .Join(_context.ArticleCategories,
                      article => article.Id,
                      articleCategory => articleCategory.ArticleId,
                      (article, articleCategory) => new Article
                      {
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          Category = new Category
                          {
                              Id = articleCategory.CategoryId
                          },
                          Provider = new Provider
                          {
                              Id = article.ProviderId
                          }
                      }
                )
                .Join(_context.Categories,
                      article => article.Category.Id,
                      category => category.Id,
                      (article, category) => new Article
                      {
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          Category = new Category
                          {
                              Id = article.Category.Id,
                              Name = category.Name
                          },
                          Provider = new Provider
                          {
                              Id = article.Provider.Id
                          }
                      }
                 )
                .Join(_context.Providers,
                      article => article.Provider.Id,
                      provider => provider.Id,
                      (article, provider) => new Article
                      {
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          Category = new Category
                          {
                              Id = article.Category.Id,
                              Name = article.Category.Name
                          },
                          Provider = new Provider
                          {
                              Id = article.Provider.Id,
                              Name = provider.Name
                          }
                      }
                 )
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return entity;
        }
    }
}
