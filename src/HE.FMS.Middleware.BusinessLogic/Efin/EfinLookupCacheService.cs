using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.Common.Settings;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HE.FMS.Middleware.BusinessLogic.Efin;

public class EfinLookupCacheService : MemoryCacheProvider<Dictionary<string, string>>, IEfinLookupCacheService
{
    private readonly IEfinLookupCosmosClient _lookupClient;

    public EfinLookupCacheService(
        IEfinLookupCosmosClient lookupClient,
        IMemoryCache memoryCache,
        MemoryCacheSettings settings)
        : base(memoryCache, settings)
    {
        _lookupClient = lookupClient;
    }

    protected override async Task<Dictionary<string, string>> RetrieveValue(string key)
    {
        var item = await _lookupClient.GetItem(key, Common.Constants.CosmosDbConfiguration.PartitonKey);

        if (item is not null && item.Value is not null)
        {
            var json = (JObject)item.Value;

            try
            {
                var dictionary = json.ToObject<Dictionary<string, string>>() ?? throw new InvalidCastException("Failed to convert JSON to Dictionary<string, string>");

                // return dictionary with case-insensitive keys  
                return new Dictionary<string, string>(dictionary, StringComparer.OrdinalIgnoreCase);
            }
            catch (JsonReaderException ex)
            {
                throw new InvalidCastException("Failed to convert JSON to Dictionary<string, string>", ex);
            }
        }

        throw new MissingCosmosDbItemException(key);
    }
}
