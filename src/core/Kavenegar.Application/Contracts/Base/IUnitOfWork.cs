namespace Kavenegar.Application.Contracts.Base
{

    public interface IUnitOfWork : IDisposable
    {

        Task<int> Commit(CancellationToken cancellationToken);

        Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

        Task Rollback();
        Task<int> SaveChangesAsync(ICurrentUserService currentUserService);
    }
}
