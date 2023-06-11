using Microsoft.Extensions.Caching.Distributed;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace MovieAdvice.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache distributedCache;
        public CacheService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }
        public async Task<T> GetData<T>(string key)
        {
            var value = await distributedCache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public async Task RemoveData(string key)
        {
            await distributedCache.RemoveAsync(key);
        }

        public async Task SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(expirtyTime);
            await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
        }
    }
}
