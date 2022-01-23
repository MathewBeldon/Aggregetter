using Aggregetter.Aggre.Application.Contracts.Persistence;
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
        public ArticleRepository(AggreDbContext context) : base(context)
        {
        }
        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
            return (await _context.Articles.OrderByDescending(o => o.Id).Take(1).SingleOrDefaultAsync(cancellationToken)).Id;
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
            var entity = await _context.Articles
                .Join(_context.Categories,
                      article => article.CategoryId,
                      category => category.Id,
                      (article, category) => new Article
                      {
                          Id = article.Id,
                          ProviderId = article.ProviderId,
                          TranslatedTitle = article.TranslatedTitle,
                          OriginalTitle = article.OriginalTitle,
                          TranslatedBody = article.TranslatedBody,
                          OriginalBody = article.OriginalBody,
                          ArticleSlug = article.ArticleSlug,
                          Endpoint = article.Endpoint,
                          CreatedDateUtc = article.CreatedDateUtc,
                          ModifiedDateUtc = article.ModifiedDateUtc,
                          Category = new Category
                          {
                              Id = category.Id,
                              Name = category.Name,                              
                          }
                      }
                )
                .Join(_context.Providers,
                      article => article.ProviderId,
                      provider => provider.Id,
                      (article, provider) => new Article
                      {
                          Id = article.Id,
                          ProviderId = article.ProviderId,
                          TranslatedTitle = article.TranslatedTitle,
                          OriginalTitle = article.OriginalTitle,
                          TranslatedBody = article.TranslatedBody,
                          OriginalBody = article.OriginalBody,
                          ArticleSlug = article.ArticleSlug,
                          Endpoint = article.Endpoint,
                          CreatedDateUtc = article.CreatedDateUtc,
                          ModifiedDateUtc = article.ModifiedDateUtc,
                          Category = new Category
                          {
                              Id = article.Category.Id,
                              Name = article.Category.Name,
                          },
                          Provider = new Provider
                          {
                              Id = provider.Id,
                              LanguageId = provider.LanguageId,
                              Name = provider.Name,
                              BaseAddress = provider.BaseAddress
                          }
                      }
                )
                .Join(_context.Languages,
                      article => article.Provider.LanguageId,
                      language => language.Id,
                      (article, language) => new Article
                      {
                          Id = article.Id,
                          ProviderId = article.ProviderId,
                          TranslatedTitle = article.TranslatedTitle,
                          OriginalTitle = article.OriginalTitle,
                          TranslatedBody = article.TranslatedBody,
                          OriginalBody = article.OriginalBody,
                          ArticleSlug = article.ArticleSlug,
                          Endpoint = article.Endpoint,
                          CreatedDateUtc = article.CreatedDateUtc,
                          ModifiedDateUtc = article.ModifiedDateUtc,
                          Category = new Category
                          {
                              Id = article.Category.Id,
                              Name = article.Category.Name,
                          },
                          Provider = new Provider
                          {
                              Id = article.Provider.Id,
                              Name = article.Provider.Name,
                              BaseAddress = article.Provider.BaseAddress,
                              Language = new Language
                              {
                                  Id = language.Id,
                                  Name = language.Name
                              }                              
                          }
                      }
                )
                .SingleOrDefaultAsync(article => article.ArticleSlug == articleSlug, cancellationToken);

            return entity;            
        }

        public async Task<List<Article>> GetArticlesByPageAsync(int page, int pageSize, int totalCount, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles
                .OrderByDescending(x => x.Id)
                .Where(a => a.Id <= totalCount - (pageSize * (page -1)))
                .Take(pageSize)
                .Join(_context.Categories,
                      article => article.CategoryId,
                      category => category.Id,
                      (article, category) => new Article
                      {
                          Id = article.Id,
                          ProviderId = article.ProviderId,
                          TranslatedTitle = article.TranslatedTitle,
                          ArticleSlug = article.ArticleSlug,
                          Endpoint = article.Endpoint,
                          Category = new Category
                          {
                              Name = category.Name,
                          }
                      }
                )
                .Join(_context.Providers,
                      article => article.ProviderId,
                      provider => provider.Id,
                      (article, provider) => new Article
                      {
                          Id = article.Id,
                          TranslatedTitle = article.TranslatedTitle,
                          ArticleSlug = article.ArticleSlug,
                          Endpoint = article.Endpoint,
                          Category = new Category
                          {
                              Name = article.Category.Name,
                          },
                          Provider = new Provider
                          {
                              Name = provider.Name,
                              BaseAddress = provider.BaseAddress
                          }
                      })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return entity;
        }
    }
}
