using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Domain.Common;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Aggregetter.Aggre.Infrastructure.MessageQueues
{
    public class TranslationQueueService<T> : ITranslationQueueService<T> where T : BaseEntity, new()
    {
        private ConnectionFactory _connectionFactory;
        private string _queue;
        private IModel _model;
        public TranslationQueueService(IConfiguration configuration)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = configuration.GetConnectionString("RabbitMQConnectionString"),
                UserName = "user",
                Password = "password",
                DispatchConsumersAsync = true
            };
            _queue = typeof(T).Name;
            _model = _connectionFactory.CreateConnection().CreateModel();

            _model.QueueDeclare(queue: _queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public bool Publish(T entity)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(entity));
            var properties = _model.CreateBasicProperties();
            properties.Persistent = true;

            _model.BasicPublish(exchange: "",
                routingKey: _queue,
                basicProperties: properties,
                body: body);

            return true;            
        }


        public T Consume()
        {
            var res = _model.BasicGet(queue: _queue,
                                 autoAck: true);

            var body = res.Body.ToArray();
            var entity = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body)) ??
                throw new ArgumentNullException();

            return entity;       
        }
    }
}
