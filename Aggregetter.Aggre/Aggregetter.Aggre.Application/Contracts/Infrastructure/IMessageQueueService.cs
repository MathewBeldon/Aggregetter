using Aggregetter.Aggre.Domain.Common;
using System.Threading;

namespace Aggregetter.Aggre.Application.Contracts.Infrastructure
{
    public interface ITranslationQueueService<T> where T : BaseEntity
    {
        bool Publish(T entity);
        T Consume();
    }
}
