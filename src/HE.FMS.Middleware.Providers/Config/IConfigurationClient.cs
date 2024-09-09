using HE.FMS.Middleware.Providers.CosmosDb;

namespace HE.FMS.Middleware.Providers.Config;
public interface IConfigurationClient
{
    Task<string> GetNextIndex(string indexName, CosmosDbItemType type);

    Task<CosmosDbConfigItem> CreateItem(string indexName, CosmosDbItemType type, string indexPrefix, int indexLength);
}
