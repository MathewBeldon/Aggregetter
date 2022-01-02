using Aggregetter.Aggre.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Contracts.Persistence
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        Task<bool> ArticleEndpointExistsAsync(string endpoint, CancellationToken cancellationToken);
        Task<bool> ArticleSlugExistsAsync(string articleSlug, CancellationToken cancellationToken);
        Task<Article> GetArticleBySlugAsync(string slug, CancellationToken cancellationToken);
    }
}
