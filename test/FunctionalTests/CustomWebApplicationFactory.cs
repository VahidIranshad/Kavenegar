using Kavenegar.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedDatabaseSetup;

namespace FunctionalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptorSDADbContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<KavenegarDbContext>));
                if (descriptorSDADbContext != null)
                {
                    services.Remove(descriptorSDADbContext);
                }
                services.AddDbContext<KavenegarDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForFunctionalTesting");
                });
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var storeDbContextSDADbContext = scopedServices.GetRequiredService<KavenegarDbContext>();
                    storeDbContextSDADbContext.Database.EnsureCreated();

                    try
                    {
                        DatabaseSetup.SeedDataDbContext(storeDbContextSDADbContext);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the Store database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }

        public void CustomConfigureServices(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Get service provider.
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var storeDbContextSDADbContext = scopedServices.GetRequiredService<KavenegarDbContext>();

                    try
                    {
                        DatabaseSetup.SeedDataDbContext(storeDbContextSDADbContext);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the Store database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}