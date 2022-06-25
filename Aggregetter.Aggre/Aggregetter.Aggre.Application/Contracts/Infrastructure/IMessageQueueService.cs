using Aggregetter.Aggre.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Contracts.Infrastructure
{
    public interface ITranslationQueueService<T> where T : BaseEntity
    {
        Task<bool> Publish(T entity, CancellationToken cancellationToken);
        Task<T> Consume(CancellationToken cancellationToken);
    }
}
