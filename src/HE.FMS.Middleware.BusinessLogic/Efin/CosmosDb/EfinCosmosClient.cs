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

    public async Task<IEnumerable<EfinItem>> GetAllNewItemsAsync(CosmosDbItemType type)
    {
        return await FindAllItems(
            x => x.Type == type && x.Status == CosmosDbItemStatus.NotProcessed,
            Common.Constants.CosmosDbConfiguration.PartitonKey);
    }

    public async Task ChangeItemsStatusAsync(IEnumerable<EfinItem> items, CosmosDbItemStatus status, CancellationToken cancellationToken)
    {
        foreach (var item in items)
        {
            await UpdateFieldAsync(item, nameof(EfinItem.Status), status, Common.Constants.CosmosDbConfiguration.PartitonKey);
        }
    }
}
