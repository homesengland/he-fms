using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;

namespace HE.FMS.Middleware.Providers.Mambu.Auth;

[ExcludeFromCodeCoverage]
internal sealed class MambuCachedApiKeyProviderDecorator : IMambuApiKeyProvider
{
    private const string CacheKey = "MambuApiKey";

    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(10);

    private readonly IMambuApiKeyProvider _decorated;

    private readonly IMemoryCache _cache;

    public MambuCachedApiKeyProviderDecorator(IMambuApiKeyProvider decorated, IMemoryCache cache)
    {
        _decorated = decorated;
        _cache = cache;
    }

    public async Task<string> GetApiKey(CancellationToken cancellationToken)
    {
        if (!_cache.TryGetValue(CacheKey, out string? secretValue))
        {
            secretValue = await _decorated.GetApiKey(cancellationToken);

            _cache.Set(CacheKey, secretValue, CacheExpiration);
        }

        return secretValue!;
    }

    public void InvalidateApiKey()
    {
        _cache.Remove(CacheKey);
    }
}
