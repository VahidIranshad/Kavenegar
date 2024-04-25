using Kavenegar.Application.Contracts.Base;
using Kavenegar.Domain.Base;
using Kavenegar.Infrastructure.Helper;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Kavenegar.Infrastructure.Repositories.Common
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;
        private readonly RedisConnectionHelper _connectionHelper;
        public CacheService(RedisConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
            _db = connectionHelper.Connection.GetDatabase();
        }
        public async Task<T> GetData<T>(int key) 
        {
            var value = await _db.StringGetAsync(typeof(T).Name + key.ToString());
            if (string.IsNullOrEmpty(value) == false)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        public async Task<bool> SetData<T>(T value) where T : BaseEntity
        {
            var isSet = await _db.StringSetAsync(typeof(T).Name + value.Id.ToString(), JsonConvert.SerializeObject(value));
            return isSet;
        }
        public async Task<bool> RemoveData<T>(int key)
        {
            bool _isKeyExist = await _db.KeyExistsAsync(typeof(T).Name + key.ToString());
            if (_isKeyExist == true)
            {
                return await _db.KeyDeleteAsync(typeof(T).Name + key.ToString());
            }
            return false;
        }
    }
}
