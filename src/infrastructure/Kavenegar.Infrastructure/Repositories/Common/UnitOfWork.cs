using Kavenegar.Application.Contracts.Base;
using Kavenegar.Domain.Base;
using Kavenegar.Infrastructure.DbContexts;
using System.Collections;

namespace Kavenegar.Infrastructure.Repositories.Common
{

    public class UnitOfWork : IUnitOfWork
    {

        private readonly KavenegarDbContext _context;


        public UnitOfWork(KavenegarDbContext context)
        {
            _context = context;
        }



        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

        public Task Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }


        public async Task<int> SaveChangesAsync(ICurrentUserService currentUserService)
        {

            return await _context.SaveChangesAsync(currentUserService);
        }

    }
}
