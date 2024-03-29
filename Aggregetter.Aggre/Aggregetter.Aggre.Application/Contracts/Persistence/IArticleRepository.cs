﻿using Aggregetter.Aggre.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Contracts.Persistence
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        Task<int> GetCount(CancellationToken cancellationToken);
        Task<int> GetCountByCategory(int categoryId, CancellationToken cancellationToken);
        Task<int> GetCountByProvider(int providerId, CancellationToken cancellationToken);
        Task<int> GetCountByProviderAndCategory(int providerId, int categoryId, CancellationToken cancellationToken);
        Task<bool> ArticleEndpointExistsAsync(string endpoint, CancellationToken cancellationToken);
        Task<bool> ArticleSlugExistsAsync(string articleSlug, CancellationToken cancellationToken);
        Task<Article> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken);
        Task<List<Article>> GetArticlesPagedAsync(int page, int pageSize, int totalCount, CancellationToken cancellationToken);
        Task<List<Article>> GetArticlesByCategoryPagedAsync(int page, int pageSize, int categoryId, CancellationToken cancellationToken);
        Task<List<Article>> GetArticlesByProviderPagedAsync(int page, int pageSize, int providerId, CancellationToken cancellationToken);
        Task<List<Article>> GetArticlesByProviderAndCategoryPagedAsync(int page, int pageSize, int providerId, int categoryId, CancellationToken cancellationToken);
        Task<int> GetSearchResultCountAsync(string search, CancellationToken cancellationToken);
        Task<List<Article>> GetSearchResultsAsync(int page, int pageSize, string search, CancellationToken cancellationToken);
    }
}
