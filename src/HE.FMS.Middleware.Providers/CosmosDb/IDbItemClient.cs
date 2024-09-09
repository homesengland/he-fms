namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface IDbItemClient
{
    Task<IList<CosmosDbItem>> GetAllNewItemsAsync(CosmosDbItemType type);

    Task UpdateItemStatusAsync(IEnumerable<CosmosDbItem> items, CosmosDbItemStatus status, CancellationToken cancellationToken);
}
