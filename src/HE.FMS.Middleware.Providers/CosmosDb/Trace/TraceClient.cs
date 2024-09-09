using HE.FMS.Middleware.Providers.CosmosDb.Settings;

namespace HE.FMS.Middleware.Providers.CosmosDb.Trace;
public class TraceClient : CosmosDbClient, ITraceClient
{
    public TraceClient(ICosmosDbSettings settings) : base(settings)
    {
    }
}
