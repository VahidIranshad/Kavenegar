using Microsoft.EntityFrameworkCore;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Exceptions;
using Kavenegar.Domain.Base;
using Kavenegar.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace Kavenegar.Infrastructure.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly KavenegarDbContext _dbContext;
        public GenericRepository(KavenegarDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> Exists(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            var data = await _dbContext.Set<T>().IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == id);
            if (data != null)
            {
                return data;
            }
            throw new NotFoundException(nameof(T), id);
        }


        public async Task<T> Add(T entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }


    }
}
