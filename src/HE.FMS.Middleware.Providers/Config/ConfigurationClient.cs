using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Providers.CosmosDb;
using Microsoft.Azure.Cosmos;

namespace HE.FMS.Middleware.Providers.Config;
public class ConfigurationClient : IConfigurationClient
{
    private readonly ICosmosDbClient _cosmosDbClient;

    public ConfigurationClient(ICosmosDbClient cosmosDbClient)
    {
        _cosmosDbClient = cosmosDbClient;
    }

    public async Task<string> GetNextIndex(string fieldName, CosmosDbItemType type)
    {
        var item = (await GetItems(fieldName, type)).FirstOrDefault() ?? throw new MissingConfigurationException($"{fieldName} in {type}");
        var nextIndex = item.GetNextIndex();
        await _cosmosDbClient.UpsertItem(item, CancellationToken.None);

        return nextIndex;
    }

    public async Task<CosmosDbConfigItem> CreateItem(string fieldName, CosmosDbItemType type, string indexPrefix, int indexLength)
    {
        var item = (await GetItems(fieldName, type)).FirstOrDefault();

        if (item is not null)
        {
            return item;
        }

        item = CosmosDbConfigItem.Create(Constants.CosmosDbConfiguration.ConfigPartitonKey, CosmosDbItemType.Claim, fieldName, indexLength, indexPrefix);
        await _cosmosDbClient.UpsertItem(item, CancellationToken.None);

        return item;
    }

    private async Task<IEnumerable<CosmosDbConfigItem>> GetItems(string fieldName, CosmosDbItemType type)
    {
        return await _cosmosDbClient.FindAllItems<CosmosDbConfigItem>(
            x => x.FieldName == fieldName && x.Type == type,
            Constants.CosmosDbConfiguration.ConfigPartitonKey);
    }
}
