using Aggregetter.Aggre.API.Middleware;
using Aggregetter.Aggre.API.Services;
using Aggregetter.Aggre.Application;
using Aggregetter.Aggre.Application.Contracts;
using Aggregetter.Aggre.Identity;
using Aggregetter.Aggre.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Aggregetter.Aggre.API
{
    public sealed class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerService();
            services.AddApplicationService(Configuration);
            services.AddPersistanceServices(Configuration);
            services.AddIdentityServices(Configuration);
            services.AddControllers();
            services.AddScoped<ILoggedInUserService, LoggedInUserServiceRegistration>();
            services.AddCachingService(Configuration, Environment);
            services.AddFeatureFlagService(Configuration);
            services.AddApiVersioningService();

            services.AddRouting(options => {
                options.LowercaseUrls = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              IHostApplicationLifetime lifetime, IDistributedCache cache)
        {
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.DisplayRequestDuration();
                    o.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggregetter API");
                });
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();  
            app.UseCors("Open");

            app.UseCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
