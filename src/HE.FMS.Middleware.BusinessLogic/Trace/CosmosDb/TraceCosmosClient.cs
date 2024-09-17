using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;

namespace HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
public sealed class TraceCosmosClient : CosmosDbClient<TraceItem>, ITraceCosmosClient
{
    public TraceCosmosClient(CosmosClient client, CosmosDbSettings settings)
        : base(client, settings)
    {
    }
}
