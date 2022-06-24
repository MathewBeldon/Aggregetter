using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Infrastructure.MessageQueue;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregetter.Aggre.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IMessageQueueService<>), typeof(MessageQueueService<>));

            return services;
        }
    }
}
