using Aggregetter.Aggre.Domain.Common;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Contracts.Infrastructure
{
    public interface IMessageQueueService<T> where T : BaseEntity
    {
        Task<bool> Publish(T entity);
        Task<T> Consume();
    }
}
