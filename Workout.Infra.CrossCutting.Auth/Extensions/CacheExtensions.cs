using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Workout.Infra.CrossCutting.Security.Extensions
{
    public static class CacheExtensions
    {
        public static void Add<T>(this IDistributedCache cache,
                                  string refreshToken,
                                  T data,
                                  TimeSpan finalExpiration)
        {
            DistributedCacheEntryOptions opcoesCache = new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(finalExpiration);
            cache.SetString(refreshToken, JsonConvert.SerializeObject(data), opcoesCache);
        }
    }
}
