using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Domain.Common;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Aggregetter.Aggre.Infrastructure.MessageQueues
{
    public class TranslationQueueService<T> : ITranslationQueueService<T> where T : BaseEntity, new()
    {
        private ConnectionFactory _connectionFactory;
        private string _queue;
        public TranslationQueueService()
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                DispatchConsumersAsync = true
            };

            _queue = typeof(T).Name;

            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            }
        }

        public Task<bool> Publish(T entity, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var connection = _connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(entity));
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                        routingKey: _queue,
                        basicProperties: properties,
                        body: body);

                    return true;
                }
            }, cancellationToken);
        }


        public Task<T> Consume(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var connection = _connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    var res = channel.BasicGet(queue: _queue,
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
