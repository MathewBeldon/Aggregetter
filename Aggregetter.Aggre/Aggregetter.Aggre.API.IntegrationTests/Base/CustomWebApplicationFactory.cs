using Aggregetter.Aggre.API.IntegrationTests.Base.Seeds;
using Aggregetter.Aggre.Identity;
using Aggregetter.Aggre.Identity.Models;
using Aggregetter.Aggre.Persistence;
using Docker.DotNet.Models;
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
        WebApplicationFactory<TStartup>, IAsyncLifetime where TStartup : class
    {
        public readonly HttpClient Client;

        private static IServiceScopeFactory _scopeFactory;
        private TestcontainerDatabase _containerData = new TestcontainersBuilder<MySqlTestcontainer>()
           .WithDatabase(new MySqlTestcontainerConfiguration
           {
               Database = "Aggregetter.Data",
               Username = "MySql",
               Password = "MyPassword",
           })
           .WithImage("mysql:8")
           .WithCleanUp(true)
           .Build();

        private TestcontainerDatabase _containerIdentity = new TestcontainersBuilder<MySqlTestcontainer>()
           .WithDatabase(new MySqlTestcontainerConfiguration
           {
               Database = "Aggregetter.Identity",
               Username = "MySql",
               Password = "MyPassword",
           })
           .WithImage("mysql:8")
           .WithCleanUp(true)
           .Build();

        public CustomWebApplicationFactory()
        {
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.ConfigureTestServices(services =>
            //{
            //    services.RemoveAll(typeof(DbContext));
            //    services.AddSingleton<>
            //});

            builder.ConfigureTestServices(services =>
            {
                var descriptorOfDbContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AggreDbContext>));

                if (descriptorOfDbContext is not null)
                {
                    services.Remove(descriptorOfDbContext);
                }

                var serverVersion = new MySqlServerVersion("8.0.0");
                services.AddDbContext<AggreDbContext>(options =>
                {
                    options.UseMySql(_containerData.ConnectionString, serverVersion, builder =>
                    {
                        builder.EnableRetryOnFailure();
                    });
                });

                //services.AddDbContext<AggreDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("AggreDbContextInMemoryDatabase");
                //});

                var descriptorOfIdentityDbContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AggreIdentityDbContext>));

                if (descriptorOfIdentityDbContext is not null)
                {
                    services.Remove(descriptorOfIdentityDbContext);
                }

                services.AddDbContext<AggreIdentityDbContext>(options =>
                {
                    options.UseMySql(_containerIdentity.ConnectionString, serverVersion, builder =>
                    {
                        builder.EnableRetryOnFailure();
                    });
                });


                //services.AddDbContext<AggreIdentityDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("AggreIdentityDbContextInMemoryDatabase");
                //});

                _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

                using var scope = _scopeFactory.CreateScope();

                var scopedService = scope.ServiceProvider;
                var dataContext = scopedService.GetRequiredService<AggreDbContext>();
                var identityContext = scopedService.GetRequiredService<AggreIdentityDbContext>();

                var logger = scopedService.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                var userManager = scopedService.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scopedService.GetRequiredService<RoleManager<IdentityRole>>();

                
                dataContext.Database.EnsureCreated();
                identityContext.Database.EnsureCreated();
                

                try
                {
                    ArticleData.Initialise(dataContext);
                    Identity.Seed.AddRoles.InitiliseAsync(roleManager).GetAwaiter().GetResult();
                    UserData.InitialiseAsync(userManager).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error seeding DB. Error: {Message}", ex.Message);
                    throw;
                }
            });
        }

        public void CreateDatabase()
        {
            
        }

        public async Task InitializeAsync()
        {
            await _containerData.StartAsync();
            await _containerIdentity.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await _containerData.DisposeAsync();
            await _containerIdentity.DisposeAsync();
        }
    }
}
