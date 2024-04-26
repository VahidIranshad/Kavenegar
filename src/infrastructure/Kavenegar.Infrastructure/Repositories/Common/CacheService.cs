using Kavenegar.Application.Contracts.Base;
using Kavenegar.Domain.Base;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Kavenegar.Infrastructure.Repositories.Common
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<T> GetData<T>(int key)
        {
            var value = await _distributedCache.GetStringAsync(typeof(T).Name + "_" + key.ToString());
            if (string.IsNullOrEmpty(value) == false)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        public async Task<bool> SetData<T>(T value) where T : BaseEntity
        {
            await _distributedCache.SetStringAsync(typeof(T).Name + "_" + value.Id.ToString(), JsonConvert.SerializeObject(value));
            return true;
        }
        public async Task<bool> RemoveData<T>(int key)
        {
            await _distributedCache.RemoveAsync(typeof(T).Name + "_" + key.ToString());
            return true;
        }
    }
}
