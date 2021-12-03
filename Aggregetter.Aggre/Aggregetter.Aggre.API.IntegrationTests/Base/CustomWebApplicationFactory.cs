using Aggregetter.Aggre.API.IntegrationTests.Base.Seeds;
using Aggregetter.Aggre.Identity;
using Aggregetter.Aggre.Identity.Models;
using Aggregetter.Aggre.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;

namespace Aggregetter.Aggre.API.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup> :
        WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                var descriptorOfDbContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AggreDbContext>));

                if (descriptorOfDbContext is not null)
                {
                    services.Remove(descriptorOfDbContext);
                }

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

                using (var scope = sp.CreateScope())
                {
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
                        await ArticleData.InitialiseAsync(dataContext);
                        await Identity.Seed.AddRoles.InitiliseAsync(roleManager);
                        await UserData.InitialiseAsync(userManager);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error seeding DB. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
