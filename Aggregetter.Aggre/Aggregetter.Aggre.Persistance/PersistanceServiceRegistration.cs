using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Persistance.Pipelines;
using Aggregetter.Aggre.Persistance.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregetter.Aggre.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(configuration.GetConnectionString("MySQLVersion"));

            services.AddDbContext<AggreDbContext>(options =>            
                options.UseMySql(configuration.GetConnectionString("AggreConnectionString"), serverVersion));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehaviour<,>));
            services.AddScoped<IArticleRepository, ArticleRepository>();

            return services;
        }
    }
}