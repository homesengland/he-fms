using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;

namespace HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;

public sealed class EfinCosmosClient : CosmosDbClient<EfinItem>, IEfinCosmosClient
{
    public EfinCosmosClient(CosmosClient client, CosmosDbSettings settings)
        : base(client, settings)
    {
    }

    public async Task<IEnumerable<EfinItem>> GetAllNewItemsAsync(CosmosDbItemType type, string environment)
    {
        var partitionKey = $"{Common.Constants.CosmosDbConfiguration.PartitonKey}-{environment}";

        return await FindAllItems(
            x => x.Type == type && x.Status == CosmosDbItemStatus.NotProcessed && x.Environment == environment,
            partitionKey);
    }

    public async Task ChangeItemsStatusAsync(IEnumerable<EfinItem> items, string environment, CosmosDbItemStatus status, CancellationToken cancellationToken)
    {
        var partitionKey = $"{Common.Constants.CosmosDbConfiguration.PartitonKey}-{environment}";

        foreach (var item in items)
        {
            await UpdateFieldAsync(item, nameof(EfinItem.Status), status, partitionKey);
        }
    }
}
