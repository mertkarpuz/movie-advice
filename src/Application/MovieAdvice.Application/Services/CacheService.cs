using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace MovieAdvice.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase redisCache;
        private readonly Configuration configuration;
        public CacheService(Configuration configuration)
        {
            this.configuration = configuration;
            var redis = ConnectionMultiplexer.Connect(configuration.ConnectionStrings.RedisConnection);
            redisCache = redis.GetDatabase();
        }
        public async Task<T> GetData<T>(string key)
        {
            var value = await redisCache.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public async Task<bool> RemoveData(string key)
        {
            var exists = await redisCache.KeyExistsAsync(key);
            if (exists)
            {
                return await redisCache.KeyDeleteAsync(key);
            }
            return false;
        }

        public async Task<bool> SetData<T>(string key, T value,DateTimeOffset expirationTime)
        {
            var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return await redisCache.StringSetAsync(key,JsonSerializer.Serialize(value),expirtyTime);
        }
    }
}
