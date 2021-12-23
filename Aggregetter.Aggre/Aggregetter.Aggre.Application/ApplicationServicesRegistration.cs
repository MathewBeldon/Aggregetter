using Aggregetter.Aggre.Application.Requests;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Aggregetter.Aggre.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            var config = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(Configuration));
           
            return services;
        }
    }
}
