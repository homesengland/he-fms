using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;

namespace HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
public interface ITraceCosmosClient : ICosmosDbClient<TraceItem>
{
}
