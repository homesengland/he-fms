using HE.FMS.Middleware.Providers.CosmosDb.Trace;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface IDbItemClient
{
    Task<IList<TraceItem>> GetAllNewItemsAsync(CosmosDbItemType type);

    Task UpdateItemStatusAsync(IEnumerable<TraceItem> items, CosmosDbItemStatus status, CancellationToken cancellationToken);
}
