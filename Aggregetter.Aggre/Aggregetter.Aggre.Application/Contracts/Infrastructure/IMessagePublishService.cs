using Aggregetter.Aggre.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Contracts.Infrastructure
{
    public interface IMessagePublishService<T> where T : BaseEntity
    {
        Task<bool> Publish(T entity, CancellationToken cancellationToken);
        T Consume();
    }
}
