using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;

namespace HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
public interface IEfinCosmosClient : ICosmosDbClient<EfinItem>
{
    Task<IEnumerable<EfinItem>> GetAllNewItemsAsync(CosmosDbItemType type, string environment);

    Task ChangeItemsStatusAsync(IEnumerable<EfinItem> items, string environment, CosmosDbItemStatus status, CancellationToken cancellationToken);
}
