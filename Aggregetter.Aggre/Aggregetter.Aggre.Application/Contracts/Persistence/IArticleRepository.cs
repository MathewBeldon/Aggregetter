using Aggregetter.Aggre.Application.Features.Articles.Queries.GetArticles;
using Aggregetter.Aggre.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Contracts.Persistence
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        Task<int> GetCount(CancellationToken cancellationToken);
        Task<bool> ArticleEndpointExistsAsync(string endpoint, CancellationToken cancellationToken);
        Task<bool> ArticleSlugExistsAsync(string articleSlug, CancellationToken cancellationToken);
        Task<Article> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken);
        Task<List<Article>> GetArticlesByPageAsync(int page, int pageSize, int totalCount, CancellationToken cancellationToken);
    }
}
