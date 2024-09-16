using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;

namespace HE.FMS.Middleware.Providers.CosmosDb.Efin;
public sealed class EfinConfigCosmosClient : CosmosDbClient<EfinConfigItem>, IEfinCosmosConfigClient
{
    public EfinConfigCosmosClient(ICosmosDbSettings settings)
        : base(settings)
    {
    }

    public async Task<EfinConfigItem> GetNextIndex(string indexName, CosmosDbItemType type)
    {
        var item = (await GetItems(indexName, type)).FirstOrDefault() ?? throw new MissingConfigurationException($"{indexName} in {type}");
        item.GetNextIndex();
        await UpsertItem(item, CancellationToken.None);

        return item;
    }

    public async Task<EfinConfigItem> CreateItem(string indexName, CosmosDbItemType type, string indexPrefix, int indexLength)
    {
        var item = (await GetItems(indexName, type)).FirstOrDefault();

        if (item is not null)
        {
            return item;
        }

        item = EfinConfigItem.Create(Constants.CosmosDbConfiguration.ConfigPartitonKey, CosmosDbItemType.Claim, indexName, indexLength, indexPrefix);
        await UpsertItem(item, CancellationToken.None);

        return item;
    }

    private async Task<IEnumerable<EfinConfigItem>> GetItems(string fieldName, CosmosDbItemType type)
    {
        return await FindAllItems(
            x => x.IndexName == fieldName && x.Type == type,
            Constants.CosmosDbConfiguration.ConfigPartitonKey);
    }
}
