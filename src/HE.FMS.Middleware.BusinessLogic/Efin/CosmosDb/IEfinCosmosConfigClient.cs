using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;

namespace HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
public interface IEfinCosmosConfigClient : ICosmosDbClient<EfinConfigItem>
{
    Task<string> GetNextIndex(string indexName, CosmosDbItemType type);

    Task<EfinConfigItem> CreateItem(string indexName, CosmosDbItemType type, string indexPrefix, int indexLength);
}
