using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;

namespace HE.FMS.Middleware.Providers.Config;
public class ConfigurationClient : CosmosDbClient, IConfigurationClient
{

    public ConfigurationClient(ICosmosDbSettings settings): base(settings)
    {
    }

    public async Task<string> GetNextIndex(string indexName, CosmosDbItemType type)
    {
        var item = (await GetItems(indexName, type)).FirstOrDefault() ?? throw new MissingConfigurationException($"{indexName} in {type}");
        var nextIndex = item.GetNextIndex();
        await UpsertItem(item, CancellationToken.None);

        return nextIndex;
    }

    public async Task<CosmosDbConfigItem> CreateItem(string indexName, CosmosDbItemType type, string indexPrefix, int indexLength)
    {
        var item = (await GetItems(indexName, type)).FirstOrDefault();

        if (item is not null)
        {
            return item;
        }

        item = CosmosDbConfigItem.Create(Constants.CosmosDbConfiguration.ConfigPartitonKey, CosmosDbItemType.Claim, indexName, indexLength, indexPrefix);
        await UpsertItem(item, CancellationToken.None);

        return item;
    }

    private async Task<IEnumerable<CosmosDbConfigItem>> GetItems(string fieldName, CosmosDbItemType type)
    {
        return await FindAllItems<CosmosDbConfigItem>(
            x => x.IndexName == fieldName && x.Type == type,
            Constants.CosmosDbConfiguration.ConfigPartitonKey);
    }
}
