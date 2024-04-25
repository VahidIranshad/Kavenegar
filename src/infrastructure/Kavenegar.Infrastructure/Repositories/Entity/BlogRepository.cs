using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Domain.Entity;
using Kavenegar.Infrastructure.DbContexts;
using Kavenegar.Infrastructure.Repositories.Common;

namespace Kavenegar.Infrastructure.Repositories.Entity
{
    public class BlogRepository : GenericRepository<BLog>, IBlogRepository
    {
        public BlogRepository(KavenegarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
