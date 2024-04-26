using Kavenegar.Application.Contracts.Base;
using Kavenegar.Domain.Entity;
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

        public virtual async Task<int> SaveChangesAsync(ICurrentUserService currentUserService)
        {
            try
            {
                /*do something*/
                /*for example base entity can have some properties like create on or Update by*/
                /* and we can fill these prperties hier*/
                /*Vahid Iranshad 2024-04-25*/

                var result = await base.SaveChangesAsync();

                return result;
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
        public DbSet<BLog> BLogDbSet { get; set; }
        public DbSet<User> UserDbSet { get; set; }

    }
}
