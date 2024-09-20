using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;

namespace HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
public interface IEfinIndexCosmosClient : ICosmosDbClient<EfinIndexItem>
{
    Task<EfinIndexItem> GetNextIndex(string indexName, CosmosDbItemType type);

    Task<EfinIndexItem> CreateItem(string indexName, CosmosDbItemType type, string indexPrefix, int indexLength);
}
