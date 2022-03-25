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

        public async Task<int> GetCountByCategory(int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Articles.Where(a => a.CategoryId == categoryId).CountAsync(cancellationToken);
        }

        public async Task<int> GetCountByProvider(int providerId, CancellationToken cancellationToken)
        {
            return await _context.Articles.Where(a => a.ProviderId == providerId).CountAsync(cancellationToken);
        }

        public async Task<int> GetCountByProviderAndCategory(int providerId, int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Articles.Where(a => a.ProviderId == providerId && a.CategoryId == categoryId).CountAsync(cancellationToken);
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
                          CreatedDateUtc = article.CreatedDateUtc,
                          ModifiedDateUtc = article.ModifiedDateUtc,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
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
                          CreatedDateUtc = article.CreatedDateUtc,
                          ModifiedDateUtc = article.ModifiedDateUtc,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
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
                          CreatedDateUtc = article.CreatedDateUtc,
                          ModifiedDateUtc = article.ModifiedDateUtc,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
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

        public async Task<List<Article>> GetArticlesPagedAsync(int page, int pageSize, int totalCount, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles
                .OrderByDescending(x => x.Id)
                .Where(a => a.Id <= totalCount - (pageSize * (page - 1)))
                .Take(pageSize)
                .Join(_context.Categories,
                      article => article.CategoryId,
                      category => category.Id,
                      (article, category) => new Article
                      {
                          Id = article.Id,
                          ProviderId = article.ProviderId,
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
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
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
                          Category = new Category
                          {
                              Id = article.Category.Id,
                              Name = article.Category.Name,
                          },
                          Provider = new Provider
                          {
                              Id = provider.Id,
                              Name = provider.Name,
                              BaseAddress = provider.BaseAddress
                          }
                      })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return entity;
        }

        public async Task<List<Article>> GetArticlesByCategoryPagedAsync(int page, int pageSize, int categoryId, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles
                .OrderByDescending(x => x.Id)
                .Where(a => a.CategoryId == categoryId)
                .Select(a => a.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Join(_context.Articles,
                      articleId => articleId,
                      article => article.Id,
                      (articleId, article) => new Article
                      {
                          Id = article.Id,
                          CategoryId = article.CategoryId,
                          ProviderId = article.ProviderId,
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
                      }
                )
                .Join(_context.Categories,
                      article => article.CategoryId,
                      category => category.Id,
                      (article, category) => new Article
                      {
                          Id = article.Id,
                          ProviderId = article.ProviderId,
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
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
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
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
                .ToListAsync(cancellationToken);

            return entity;
        }

        public async Task<List<Article>> GetArticlesByProviderPagedAsync(int page, int pageSize, int providerId, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles
                .OrderByDescending(x => x.Id)
                .Where(a => a.ProviderId == providerId)
                .Select(a => a.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Join(_context.Articles,
                      articleId => articleId,
                      article => article.Id,
                      (articleId, article) => new Article
                      {
                          Id = article.Id,
                          CategoryId = article.CategoryId,
                          ProviderId = article.ProviderId,
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
                      }
                )
                .Join(_context.Categories,
                      article => article.CategoryId,
                      category => category.Id,
                      (article, category) => new Article
                      {
                          Id = article.Id,
                          ProviderId = article.ProviderId,
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
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
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
                          Category = new Category
                          {
                              Id = article.Category.Id,
                              Name = article.Category.Name,
                          },
                          Provider = new Provider
                          {
                              Id = provider.Id,
                              Name = provider.Name,
                              BaseAddress = provider.BaseAddress
                          }
                      })
                .ToListAsync(cancellationToken);

            return entity;
        }

        public async Task<List<Article>> GetArticlesByProviderAndCategoryPagedAsync(int page, int pageSize, int providerId, int categoryId, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles
                .OrderByDescending(x => x.Id)
                .Where(a => a.ProviderId == providerId && a.CategoryId == categoryId)
                .Select(a => a.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Join(_context.Articles,
                      articleId => articleId,
                      article => article.Id,
                      (articleId, article) => new Article
                      {
                          Id = article.Id,
                          CategoryId = article.CategoryId,
                          ProviderId = article.ProviderId,
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
                      }
                )
                .Join(_context.Categories,
                      article => article.CategoryId,
                      category => category.Id,
                      (article, category) => new Article
                      {
                          Id = article.Id,
                          ProviderId = article.ProviderId,
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
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
                          ArticleSlug = article.ArticleSlug,
                          TranslatedTitle = article.TranslatedTitle,
                          TranslatedDateUtc = article.TranslatedDateUtc,
                          Endpoint = article.Endpoint,
                          Category = new Category
                          {
                              Id = article.Category.Id,
                              Name = article.Category.Name,
                          },
                          Provider = new Provider
                          {
                              Id = provider.Id,
                              Name = provider.Name,
                              BaseAddress = provider.BaseAddress
                          }
                      })
                .ToListAsync(cancellationToken);

            return entity;
        }

        public Task<int> GetSearchResultCountAsync(string search, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Article>> GetSearchResultsAsync(int page, int pageSize, string search, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}