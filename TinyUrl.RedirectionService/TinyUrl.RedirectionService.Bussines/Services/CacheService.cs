using Microsoft.Extensions.Caching.Memory;
using TinyUrl.RedirectionService.Infrastructure.Services;

namespace TinyUrl.RedirectionService.Bussines.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public object GetValue(string key)
        {
            _cache.TryGetValue(key, out var result);

            return result!;
        }

        public void SetValue(string key, object value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromHours(1));

            _cache.Set(key, value, cacheEntryOptions);
        }

    }
}
