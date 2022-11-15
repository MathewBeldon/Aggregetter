using Aggregetter.Aggre.Application.Pipelines.Caching;
using Aggregetter.Aggre.Application.Pipelines.Pagination;
using Aggregetter.Aggre.Application.Pipelines.Validation;
using Aggregetter.Aggre.Application.Services.PaginationService;
using Aggregetter.Aggre.Application.Settings;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Aggregetter.Aggre.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingPipelineBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PaginationPipelineBehaviour<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient<IPaginationService, PaginationService>();
            services.AddSingleton(new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            });

            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
            services.Configure<PagedSettings>(configuration.GetSection("PagedSettings"));

            return services;
        }
    }
}
