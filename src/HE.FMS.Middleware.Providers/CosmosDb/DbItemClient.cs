using HE.FMS.Middleware.Common;
using Microsoft.Azure.Cosmos;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public class DbItemClient : IDbItemClient
{
    private readonly ICosmosDbClient _cosmosDbClient;

    public DbItemClient(ICosmosDbClient cosmosDbClient)
    {
        _cosmosDbClient = cosmosDbClient;
    }

    public async Task<IList<CosmosDbItem>> GetAllNewItemsAsync(CosmosDbItemType type)
    {
        return await _cosmosDbClient.FindAllItems<CosmosDbItem>(
            x => x.Status == CosmosDbItemStatus.New && x.Type == type,
            Constants.CosmosDbConfiguration.PartitonKey);
    }

    public async Task UpdateItemStatusAsync(IEnumerable<CosmosDbItem> items, CosmosDbItemStatus status, CancellationToken cancellationToken)
    {
        foreach (var item in items)
        {
            await _cosmosDbClient.UpdateFieldAsync(item, nameof(CosmosDbItem.Status), status, Constants.CosmosDbConfiguration.PartitonKey);
        }
    }
}
