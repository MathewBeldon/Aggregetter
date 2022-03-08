using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregetter.Aggre.API.Services.Registration
{
    internal static class ApiVersioningServiceRegistration
    {
        internal static IServiceCollection AddApiVersioningService(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            return services;
        }
    }
}


