using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Infrastructure.DbContexts;
using Kavenegar.Infrastructure.Repositories.Common;

namespace Kavenegar.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KavenegarDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("KavenegarConnectionStrings")),
               ServiceLifetime.Scoped
               );
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return services;
        }

        public class QuestionnaireDbContextFactor : IDesignTimeDbContextFactory<KavenegarDbContext>
        {
            public KavenegarDbContext CreateDbContext(string[] args)
            {

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var builder = new DbContextOptionsBuilder<KavenegarDbContext>();
                var connectionString = configuration.GetConnectionString("KavenegarConnectionStrings");
                builder.UseSqlServer(connectionString);
                return new KavenegarDbContext(builder.Options);

            }
        }
    }
}
