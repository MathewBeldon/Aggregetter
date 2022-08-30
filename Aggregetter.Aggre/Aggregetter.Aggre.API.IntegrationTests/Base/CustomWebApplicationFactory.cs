using Aggregetter.Aggre.API.IntegrationTests.Base.Seeds;
using Aggregetter.Aggre.Identity;
using Aggregetter.Aggre.Identity.Models;
using Aggregetter.Aggre.Persistence;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Aggregetter.Aggre.API.IntegrationTests.Base
{
    public sealed class CustomWebApplicationFactory<TStartup> :
        WebApplicationFactory<TStartup> where TStartup : class
    {
        public readonly HttpClient Client;

        //private readonly TestcontainerDatabase _container;

        public CustomWebApplicationFactory()
        {
           // _container = new TestcontainersBuilder<MySqlTestcontainer>()
           //.WithDatabase(new MySqlTestcontainerConfiguration
           //{
           //    Database = "Aggregetter.Data",
           //    Username = "MySql",
           //    Password = "MyPassword",
           //})
           //.WithImage("mysql:8")
           //.WithCleanUp(true)
           //.WithPortBinding(33306, 3306)
           //.Build();

            Client = CreateClient();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.ConfigureTestServices(services =>
            //{
            //    services.RemoveAll(typeof(DbContext));
            //    services.AddSingleton<>
            //});

            builder.ConfigureTestServices(async services =>
            {
                var descriptorOfDbContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AggreDbContext>));

                if (descriptorOfDbContext is not null)
                {
                    services.Remove(descriptorOfDbContext);
                }

                //var serverVersion = new MySqlServerVersion("8.0.0");
                //services.AddDbContext<AggreDbContext>(options =>
                //{
                //    options.UseMySql("Server=localhost;Port=33306;Database=Aggregetter.Data;Uid=MySql;Pwd=MyPassword;ConnectionTimeout=0;DefaultCommandTimeout=0;", serverVersion, builder =>
                //    {
                //        builder.EnableRetryOnFailure();
                //    });
                //});

                services.AddDbContext<AggreDbContext>(options =>
                {
                    options.UseInMemoryDatabase("AggreDbContextInMemoryDatabase");
                });

                var descriptorOfIdentityDbContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AggreIdentityDbContext>));

                if (descriptorOfIdentityDbContext is not null)
                {
                    services.Remove(descriptorOfIdentityDbContext);
                }
                
                services.AddDbContext<AggreIdentityDbContext>(options =>
                {
                    options.UseInMemoryDatabase("AggreIdentityDbContextInMemoryDatabase");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var scopedService = scope.ServiceProvider;
                var dataContext = scopedService.GetRequiredService<AggreDbContext>();
                var identityContext = scopedService.GetRequiredService<AggreIdentityDbContext>();

                var logger = scopedService.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                var userManager = scopedService.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scopedService.GetRequiredService<RoleManager<IdentityRole>>();

                await dataContext.Database.EnsureCreatedAsync();
                await identityContext.Database.EnsureCreatedAsync();

                try
                {
                    await ArticleData.InitialiseAsync(dataContext);
                    await Identity.Seed.AddRoles.InitiliseAsync(roleManager);
                    await UserData.InitialiseAsync(userManager);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error seeding DB. Error: {Message}", ex.Message);
                }
            });
        }

        //public async Task InitializeAsync() => await _container.StartAsync();

        //public new async Task DisposeAsync() => await _container.DisposeAsync();
    }
}
