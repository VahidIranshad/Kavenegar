using Kavenegar.Domain.Entity;
using Kavenegar.Infrastructure.DbContexts;
using SharedDatabaseSetup.Common;
namespace SharedDatabaseSetup.Entity
{
    internal class BlogSeed
    {
        public async static Task SeedData(KavenegarDbContext context)
        {
            context.BLogDbSet.RemoveRange(context.BLogDbSet);
            var list = new List<BLog> {
                new BLog{Id = 1, Title = "TitleTest1", Content= "ContentTest1", AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id},
                new BLog{Id = 2, Title = "TitleTest2", Content= "ContentTest2", AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id},
                new BLog{Id = 3, Title = "TitleTest3", Content= "ContentTest3", AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id},
                new BLog{Id = 4, Title = "TitleTest4", Content= "ContentTest4", AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id},
            };
            context.AddRange(list);
            await context.SaveChangesAsync(DefaultCurrentUserService.adminCurrentUserService.Object);


        }
    }
}
