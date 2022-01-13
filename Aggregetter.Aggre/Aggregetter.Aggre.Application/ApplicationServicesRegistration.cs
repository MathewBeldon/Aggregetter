using Aggregetter.Aggre.Application.Features.Pipelines.Caching;
using Aggregetter.Aggre.Application.Features.Pipelines.Validation;
using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aggregetter.Aggre.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingPipelineBehavior<,>));
            services.AddSingleton<IPaginationService, PaginationService>();

            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
            services.Configure<PagedSettings>(configuration.GetSection("PagedSettings"));

            return services;
        }
    }
}
