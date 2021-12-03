using Aggregetter.Aggre.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Contracts.Persistence
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        Task<bool> IsArticleEndpointUniqueAsync(string endpoint, CancellationToken cancellationToken);
    }
}
