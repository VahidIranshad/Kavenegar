using Kavenegar.Infrastructure.DbContexts;
using SharedDatabaseSetup.Entity;

namespace SharedDatabaseSetup
{
    public static class DatabaseSetup
    {
        static object locker = new object();
        static object test;
        public async static void SeedDataDbContext(KavenegarDbContext context)
        {
            if (test != null)
            {
                return;
            }
            List<Task> tasks = new List<Task>();
            lock (locker)
            {
                if (test != null)
                {
                    return;
                }
                UserSeed.SeedData(context);
                BlogSeed.SeedData(context);

                test = new object();
            }

        }
    }
}
