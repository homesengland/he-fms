using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;

namespace HE.FMS.Middleware.Providers.CosmosDb.Efin;
public interface IEfinCosmosConfigClient : ICosmosDbClient<EfinConfigItem>
{
    Task<EfinConfigItem> GetNextIndex(string indexName, CosmosDbItemType type);

    Task<EfinConfigItem> CreateItem(string indexName, CosmosDbItemType type, string indexPrefix, int indexLength);
}
