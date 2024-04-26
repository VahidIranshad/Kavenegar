using Kavenegar.Domain.Entity;
using Kavenegar.Infrastructure.DbContexts;
using SharedDatabaseSetup.Common;

namespace SharedDatabaseSetup.Entity
{
    internal class UserSeed
    {
        public async static Task SeedData(KavenegarDbContext context)
        {
            context.UserDbSet.RemoveRange(context.UserDbSet);
            var list = new List<User> {
                new User{Id = Kavenegar.Domain.Const.UserConst.Admin_Id, Name = Kavenegar.Domain.Const.UserConst.Admin_Name},
            };
            context.AddRange(list);
            await context.SaveChangesAsync(DefaultCurrentUserService.adminCurrentUserService.Object);


        }
    }
}
