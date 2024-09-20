using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;

namespace HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
public sealed class EfinIndexCosmosClient : CosmosDbClient<EfinIndexItem>, IEfinIndexCosmosClient
{
    public EfinIndexCosmosClient(CosmosClient client, CosmosDbSettings settings)
        : base(client, settings)
    {
    }

    public async Task<EfinIndexItem> GetNextIndex(string indexName, CosmosDbItemType type)
    {
        var item = (await GetItems(indexName, type)).FirstOrDefault() ?? throw new MissingConfigurationException($"{indexName} in {type}");
        item.GetNextId();
        await UpsertItem(item, CancellationToken.None);

        return item;
    }

    public async Task<EfinIndexItem> CreateItem(string indexName, CosmosDbItemType type, string indexPrefix, int indexLength)
    {
        var item = (await GetItems(indexName, type)).FirstOrDefault();

        if (item is not null)
        {
            return item;
        }

        item = EfinIndexItem.Create(Common.Constants.CosmosDbConfiguration.PartitonKey, type, indexName, indexLength, indexPrefix);
        await UpsertItem(item, CancellationToken.None);

        return item;
    }

    private async Task<IEnumerable<EfinIndexItem>> GetItems(string fieldName, CosmosDbItemType type)
    {
        return await FindAllItems(
            x => x.IndexName == fieldName && x.Type == type,
            Common.Constants.CosmosDbConfiguration.PartitonKey);
    }
}
