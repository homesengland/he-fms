using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;

namespace HE.FMS.Middleware.Providers.CosmosDb.Efin;

public sealed class EfinCosmosClient : CosmosDbClient<EfinItem>, IEfinCosmosClient
{
    public EfinCosmosClient(ICosmosDbSettings settings)
        : base(settings)
    {
    }

    public async Task<IEnumerable<EfinItem>> GetAllNewItemsAsync(CosmosDbItemType type)
    {
        return await FindAllItems(
            x => x.Type == type && x.Status == CosmosDbItemStatus.New,
            Constants.CosmosDbConfiguration.PartitonKey);
    }

    public async Task ChangeItemsStatusAsync(IEnumerable<EfinItem> items, CosmosDbItemStatus status, CancellationToken cancellationToken)
    {
        foreach (var item in items)
        {
            await UpdateFieldAsync(item, nameof(EfinItem.Status), status, Constants.CosmosDbConfiguration.PartitonKey);
        }
    }
}
