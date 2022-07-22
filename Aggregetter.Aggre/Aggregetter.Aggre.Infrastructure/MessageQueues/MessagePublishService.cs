using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Domain.Common;
using MassTransit;

namespace Aggregetter.Aggre.Infrastructure.MessageQueues
{
    public class MessagePublishService<T> : IMessagePublishService<T> where T : BaseEntity, new()
    {
        readonly IPublishEndpoint _publishEndpoint;

        public MessagePublishService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<bool> Publish(T entity, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(entity, cancellationToken);

            return true;            
        }

        public T Consume()
        {
            throw new NotImplementedException();     
        }
    }
}
