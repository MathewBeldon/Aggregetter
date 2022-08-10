using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Infrastructure.MessageQueues;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregetter.Aggre.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(IMessagePublishService<>), typeof(MessagePublishService<>));
            services.AddMassTransit(mt =>
            {
                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMQConnectionString"), "/", h => {
                        h.Username("user");
                        h.Password("password");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
