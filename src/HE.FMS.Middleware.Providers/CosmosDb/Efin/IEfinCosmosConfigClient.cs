using HE.FMS.Middleware.Providers.CosmosDb.Base;

namespace HE.FMS.Middleware.Providers.CosmosDb.Efin;
public interface IEfinCosmosConfigClient : ICosmosDbClient<EfinConfigItem>
{
    Task<string> GetNextIndex(string indexName, CosmosDbItemType type);

    Task<EfinConfigItem> CreateItem(string indexName, CosmosDbItemType type, string indexPrefix, int indexLength);
}
