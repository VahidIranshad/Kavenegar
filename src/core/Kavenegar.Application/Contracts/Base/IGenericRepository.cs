using Kavenegar.Domain.Base;
using System.Linq.Expressions;

namespace Kavenegar.Application.Contracts.Base
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Find(int id);
        Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> Add(T entity);
        Task<bool> Exists(int id);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
