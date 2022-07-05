using Aggregetter.Aggre.Identity;
using Aggregetter.Aggre.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API
{
    public sealed class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();                       

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var config = services.GetRequiredService<IConfiguration>();

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(config.GetConnectionString("ElasticLogConnectionString")))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7
                    })
                    .CreateLogger();

                try
                {
                    var aggreDbcontext = services.GetRequiredService<AggreDbContext>();
                    if (aggreDbcontext.Database.GetPendingMigrations().Any())
                    {
                        aggreDbcontext.Database.Migrate();
                    }

                    var aggreIdentityDbcontext = services.GetRequiredService<AggreIdentityDbContext>();
                    if (aggreIdentityDbcontext.Database.GetPendingMigrations().Any())
                    {
                        aggreIdentityDbcontext.Database.Migrate();
                    }
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await Identity.Seed.AddRoles.InitiliseAsync(roleManager);
                    await Persistence.Seed.AddDataButFast.InitiliseAsync(aggreDbcontext, Log.Logger);
                    Log.Information("Application Starting");
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "An error occured while starting the application");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
