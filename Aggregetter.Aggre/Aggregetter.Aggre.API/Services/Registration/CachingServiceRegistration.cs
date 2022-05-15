using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aggregetter.Aggre.API.Services.Registration
{
    internal static class CachingServiceRegistration
    {
        internal static IServiceCollection AddCachingService(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (configuration.GetConnectionString("RedisConnectionString").Length == 0)
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration.GetConnectionString("RedisConnectionString");
                    options.InstanceName = "SampleInstance";
                });
            }
            return services;
        }
    }
}
