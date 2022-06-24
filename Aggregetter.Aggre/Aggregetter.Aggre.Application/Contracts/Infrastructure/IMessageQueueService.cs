using Aggregetter.Aggre.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Contracts.Infrastructure
{
    public interface IMessageQueueService<T> where T : BaseEntity
    {
        Task<bool> Publish(T entity, string queue, CancellationToken cancellationToken);
        Task<T> Consume(string queue, CancellationToken cancellationToken);
    }
}
