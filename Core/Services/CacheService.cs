
using Domain.Contracts;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Services
{
    public class CacheService(ICacheRepository cacheRepository)
        : ICacheService
    {
        public async Task<string?> GetCachedValueAsync(string cacheKey)
            => await cacheRepository.GetAsync(cacheKey);

        public async Task SetCacheValueAsync(string cacheKey, object value, TimeSpan duration)
            => await cacheRepository.SetAsync(cacheKey, value, duration);
    }
}
