using System;
using System.Threading.Tasks;

namespace Tweetbook.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(String cacheKey, object response, TimeSpan timeTimeLive);

        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
