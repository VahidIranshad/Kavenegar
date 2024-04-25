using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Domain.Entity;
using Kavenegar.Infrastructure.DbContexts;
using Kavenegar.Infrastructure.Repositories.Common;

namespace Kavenegar.Infrastructure.Repositories.Entity
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(KavenegarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
