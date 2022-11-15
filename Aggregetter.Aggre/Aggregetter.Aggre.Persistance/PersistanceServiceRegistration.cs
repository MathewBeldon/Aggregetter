using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Persistence.Pipelines;
using Aggregetter.Aggre.Persistence.Repositories;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregetter.Aggre.Persistence
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(configuration.GetConnectionString("MySQLVersion"));
            services.AddDbContext<AggreDbContext>(options =>            
                options.UseMySql(configuration.GetConnectionString("AggreConnectionString"), serverVersion, builder =>
                {
                    builder.EnableRetryOnFailure();
                }));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));            
            services.AddScoped<IArticleRepository, ArticleRepository>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehaviour<,>));

            return services;
        }
    }
}