using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;

namespace HE.FMS.Middleware.Providers.CosmosDb.Trace;
public sealed class TraceCosmosClient : CosmosDbClient<TraceItem>, ITraceCosmosClient
{
    public TraceCosmosClient(ICosmosDbSettings settings)
        : base(settings)
    {
    }
}
