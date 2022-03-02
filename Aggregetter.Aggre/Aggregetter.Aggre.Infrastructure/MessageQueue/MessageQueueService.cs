using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Domain.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Aggregetter.Aggre.Infrastructure.MessageQueue
{
    internal class MessageQueueService<T> : IMessageQueueService<T> where T : BaseEntity
    {
        public Task<bool> Publish(T entity)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: "logs",
                                  routingKey: "");

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] {0}", message);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
            return Task.FromResult(true);
        }
    }
}
