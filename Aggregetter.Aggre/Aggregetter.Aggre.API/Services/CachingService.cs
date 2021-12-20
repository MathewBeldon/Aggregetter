using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aggregetter.Aggre.API.Services
{
    internal static class CachingServiceRegistration
    {
        internal static IServiceCollection AddCachingService(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {            
            if (environment.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration["Redis:ConnectionString"];
                    options.InstanceName = "SampleInstance";
                });
            }
            return services;
        }
    }
}
