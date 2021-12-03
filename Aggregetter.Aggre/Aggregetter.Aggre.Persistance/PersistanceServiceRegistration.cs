using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Persistance.Repositories;
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
            services.AddScoped<IArticleRepository, ArticleRepository>();

            return services;
        }
    }
}