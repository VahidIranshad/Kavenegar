using AutoMapper;
using Kavenegar.Application.Profiles;
using Kavenegar.Infrastructure.DbContexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SharedDatabaseSetup;
using System.Data.Common;

namespace IntegrationTests
{
    public class SharedDatabaseFixture : IDisposable
    {

        private static IConfigurationProvider _configuration;
        public static IConfigurationProvider configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<MappingProfile>();
                    });
                }
                return _configuration;
            }
        }
        private static IMapper _mapper;
        public static IMapper mapper
        {
            get
            {
                if (_mapper == null)
                {
                    _mapper = configuration.CreateMapper();
                }
                return _mapper;
            }

        }

        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        private string dbNameDbContext = "IntegrationTestsDatabase.db";

        public SharedDatabaseFixture()
        {
            ConnectionDbContext = new SqliteConnection($"Filename={dbNameDbContext}");

            SeedDbContext();

            ConnectionDbContext.Open();

        }

        public DbConnection ConnectionDbContext { get; }

        public KavenegarDbContext CreateContext(DbTransaction? transaction = null)
        {
            var context = new KavenegarDbContext(new DbContextOptionsBuilder<KavenegarDbContext>().UseSqlite(ConnectionDbContext).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void SeedDbContext()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        DatabaseSetup.SeedDataDbContext(context);
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public void Dispose()
        {
            ConnectionDbContext.Dispose();
        }
    }
}
