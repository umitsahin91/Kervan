namespace Kervan.SharedKernel.Application.Interfaces;

public interface ICacheService
{
    /// <summary>
    /// Verilen anahtara sahip veriyi önbellekten getirmeye çalışır.
    /// </summary>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Bir veriyi, belirtilen süre boyunca önbelleğe alır.
    /// </summary>
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Bir veriyi önbellekten siler.
    /// </summary>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}