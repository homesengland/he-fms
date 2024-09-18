using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;

namespace HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
public interface IEfinLookupCosmosClient : ICosmosDbClient<EfinLookupItem>
{
}
