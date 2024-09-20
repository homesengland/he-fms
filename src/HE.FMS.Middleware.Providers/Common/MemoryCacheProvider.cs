using HE.FMS.Middleware.Providers.Common.Settings;
using Microsoft.Extensions.Caching.Memory;

namespace HE.FMS.Middleware.Providers.Common;

public abstract class MemoryCacheProvider<T> : IMemoryCacheProvider<T>
    where T : class
{
    private readonly IMemoryCache _cache;
    private readonly int _tokenExpirationTimeInMinutes;

    protected MemoryCacheProvider(IMemoryCache cache, MemoryCacheSettings settings)
    {
        _cache = cache;

        _tokenExpirationTimeInMinutes = settings?.TokenExpirationTimeInMinutes ?? DateTimeOffset.MinValue.AddDays(1).TotalOffsetMinutes;
    }

    public async Task<T> GetValue(string key)
    {
        var value = (T?)_cache.Get(key) ?? default;

        if (object.Equals(value, default(T)))
        {
            value = await RetrieveValue(key);

            _cache.Set(key, value, DateTimeOffset.Now.AddMinutes(_tokenExpirationTimeInMinutes));
        }

        return value;
    }

    public void InvalidateKey(string key)
    {
        _cache.Remove(key);
    }

    protected abstract Task<T> RetrieveValue(string key);
}
