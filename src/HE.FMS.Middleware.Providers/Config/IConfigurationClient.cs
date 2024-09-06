using HE.FMS.Middleware.Providers.CosmosDb;

namespace HE.FMS.Middleware.Providers.Config;
public interface IConfigurationClient
{
    Task<string> GetNextIndex(string fieldName, CosmosDbItemType type);

    Task<CosmosDbConfigItem> CreateItem(string fieldName, CosmosDbItemType type, string indexPrefix, int indexLength);
}
