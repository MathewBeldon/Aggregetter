using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Aggregetter.Aggre.API.Services.Registration
{
    internal static class FeatureFlagServiceRegistration
    {
        internal static IServiceCollection AddFeatureFlagService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFeatureManagement(configuration.GetSection("FeatureManagement"));
            return services;
        }
    }
}


