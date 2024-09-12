using HE.FMS.Middleware.Providers.CosmosDb.Base;

namespace HE.FMS.Middleware.Providers.CosmosDb.Efin;
public interface IEfinCosmosClient : ICosmosDbClient<EfinItem>
{
    Task<IEnumerable<EfinItem>> GetAllNewItemsAsync(CosmosDbItemType type);

    Task ChangeItemsStatusAsync(IEnumerable<EfinItem> items, CosmosDbItemStatus status, CancellationToken cancellationToken);
}
