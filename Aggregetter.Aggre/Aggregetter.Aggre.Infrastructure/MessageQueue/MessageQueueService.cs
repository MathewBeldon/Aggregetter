using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Domain.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Aggregetter.Aggre.Infrastructure.MessageQueue
{
    public class MessageQueueService<T> : IMessageQueueService<T> where T : BaseEntity, new()
    {
        private ConnectionFactory _connectionFactory;
        public MessageQueueService()
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                DispatchConsumersAsync = true
            };
        }

        public Task<bool> Publish(T entity, string queue, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var connection = _connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue,
                                                 durable: true,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);

                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(entity));
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish(exchange: "",
                        routingKey: queue,
                        basicProperties: properties,
                        body: body);

                    return true;
                }
            }, cancellationToken);
        }


        public Task<T> Consume(string queue, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var connection = _connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var res = channel.BasicGet(queue: queue,
                                         autoAck: true);

                    var body = res.Body.ToArray();
                    var entity = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body)) ??
                        throw new ArgumentNullException();

                    return entity;
                }
            }, cancellationToken);
        }
    }
}
