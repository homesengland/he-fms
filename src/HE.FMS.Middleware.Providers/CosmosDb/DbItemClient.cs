using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Providers.CosmosDb.Trace;
using Microsoft.Azure.Cosmos;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public class DbItemClient : IDbItemClient
{
    private readonly ICosmosDbClient _cosmosDbClient;

    public DbItemClient(ICosmosDbClient cosmosDbClient)
    {
        _cosmosDbClient = cosmosDbClient;
    }

    public async Task<IList<TraceItem>> GetAllNewItemsAsync(CosmosDbItemType type)
    {
        return await _cosmosDbClient.FindAllItems<TraceItem>(
            x => x.Type == type,
            Constants.CosmosDbConfiguration.PartitonKey);
    }

    public async Task UpdateItemStatusAsync(IEnumerable<TraceItem> items, CosmosDbItemStatus status, CancellationToken cancellationToken)
    {
        foreach (var item in items)
        {
            // TODO: FIELDNAME IS DELETED
            await _cosmosDbClient.UpdateFieldAsync(item, "", status, Constants.CosmosDbConfiguration.PartitonKey);
        }
    }
}
