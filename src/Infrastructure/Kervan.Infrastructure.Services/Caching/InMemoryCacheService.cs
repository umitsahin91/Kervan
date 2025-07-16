using Kervan.Core.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Kervan.Infrastructure.Services.Caching;

public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        return Task.FromResult(_memoryCache.Get<T>(key));
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        _memoryCache.Remove(key);
        return Task.CompletedTask;
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
    {
        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
        {
            options.SetAbsoluteExpiration(expiration.Value);
        }
        else
        {
            // Varsayılan süre, örneğin 10 dakika
            options.SetSlidingExpiration(TimeSpan.FromMinutes(10));
        }

        _memoryCache.Set(key, value, options);
        return Task.CompletedTask;
    }
}