using Aggregetter.Aggre.Application.Contracts.Infrastructure;
using Aggregetter.Aggre.Infrastructure.MessageQueues;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregetter.Aggre.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ITranslationQueueService<>), typeof(TranslationQueueService<>));

            return services;
        }
    }
}
