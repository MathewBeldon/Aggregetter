using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Domain.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

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
                Password = "password"
            };
        }

        public Task<bool> Publish(T entity)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "test_queue",
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                for (int i = 0; i < 100; i++)
                {
                    channel.BasicPublish(exchange: "",
                                     routingKey: "test_queue",
                                     basicProperties: properties,
                                     body: body);
                }

                Console.WriteLine(" [x] Sent {0}", message);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

            return Task.FromResult(true);
        }


        public Task<T> Consume()
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "test_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "test_queue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

            return Task.FromResult(new T());
        }
    }
}
