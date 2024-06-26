﻿using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Domain.Entity;
using Microsoft.VisualBasic;
using StackExchange.Redis;
using System.Linq.Expressions;

namespace Kavenegar.Infrastructure.Repositories.Entity
{
    public class CachedBlogRepository : IBlogRepository
    {
        public readonly ICacheService _cacheService;
        public IBlogRepository _decorated;

        public CachedBlogRepository(ICacheService cacheService, IBlogRepository decorated)
        {
            _cacheService = cacheService;
            _decorated = decorated;
        }

        public async Task<BLog> Add(BLog entity) => await _decorated.Add(entity);

        public async Task Delete(BLog entity)
        {
            await _decorated.Delete(entity);
            await _cacheService.RemoveData<BLog>(entity.Id);
        }

        public async Task Update(BLog entity)
        {
            await _decorated.Update(entity);
            await _cacheService.RemoveData<BLog>(entity.Id);
        }

        public async Task<bool> Exists(int id)
        {
            var data = await Get(id);
            if (data == null)
            {
                return false;
            }
            return true;
        }

        public async Task<BLog> Get(int id)
        {

            var result = await _cacheService.GetData<BLog>(id);
            if (result == null)
            {
                result = await _decorated.Get(id);
                if (result != null)
                {
                    await _cacheService.SetData(result);
                }
            }
            return result;
        }

        public async Task<IReadOnlyList<BLog>> Get(Expression<Func<BLog, bool>> predicate = null, Func<IQueryable<BLog>, IOrderedQueryable<BLog>> orderBy = null)
        {
            return await _decorated.Get(predicate, orderBy);
        }

        public async Task<IReadOnlyList<BLog>> GetAll() => await _decorated.GetAll();

        public async Task<BLog> Find(int id)
        {
            return await _decorated.Find(id);
        }


        //public async Task<BLog> Add(BLog entity)
        //{
        //    var data = await _blogRepository.Add(entity);
        //    var result = await _cacheService.SetData(data);
        //    if (result == false)
        //    {
        //        throw new Exception("Can not set data in Cache");
        //    }
        //    return data;

        //}

        //public async Task Update(BLog entity)
        //{
        //    await _blogRepository.Update(entity);
        //    var result = await _cacheService.SetData(entity);
        //    if (result == false)
        //    {
        //        throw new Exception("Can not set data in Cache");
        //    }
        //    return;
        //}

        //public async Task Delete(BLog entity)
        //{
        //    await _blogRepository.Delete(entity);
        //    var result = await _cacheService.RemoveData<BLog>(entity.Id);
        //    if (result == false)
        //    {
        //        throw new Exception("Can not set data in Cache");
        //    }
        //    return;
        //}
    }
}
