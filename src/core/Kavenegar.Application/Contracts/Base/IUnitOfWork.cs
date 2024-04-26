namespace Kavenegar.Application.Contracts.Base
{

    public interface IUnitOfWork : IDisposable
    {

        Task<int> Commit(CancellationToken cancellationToken);
        Task Rollback();
        Task<int> SaveChangesAsync(ICurrentUserService currentUserService);
    }
}
