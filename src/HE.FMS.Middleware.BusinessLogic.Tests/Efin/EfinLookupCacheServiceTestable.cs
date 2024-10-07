using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.Common.Settings;
using Microsoft.Extensions.Caching.Memory;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;

public class EfinLookupCacheServiceTestable : EfinLookupCacheService
{
    public EfinLookupCacheServiceTestable(
        IEfinLookupCosmosClient lookupClient,
        IMemoryCache memoryCache,
        MemoryCacheSettings settings)
        : base(lookupClient, memoryCache, settings)
    {
    }

    public new async Task<Dictionary<string, string>> RetrieveValue(string key)
    {
        return await base.RetrieveValue(key);
    }
}
