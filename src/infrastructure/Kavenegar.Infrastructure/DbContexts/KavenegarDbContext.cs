using Microsoft.EntityFrameworkCore;

namespace Kavenegar.Infrastructure.DbContexts
{
    public class KavenegarDbContext : DbContext
    {
        public KavenegarDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KavenegarDbContext).Assembly);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            try
            {
                /*do something*/
                var result = await base.SaveChangesAsync();

                return result;
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

    }
}
