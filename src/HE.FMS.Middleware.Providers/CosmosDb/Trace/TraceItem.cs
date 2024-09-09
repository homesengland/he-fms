using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Providers.CosmosDb.Base;

namespace HE.FMS.Middleware.Providers.CosmosDb.Trace;
public class TraceItem : DbItem
{
    public static TraceItem CreateTraceItem(object value, string idempotencyKey, CosmosDbItemType type)
    {
        return new TraceItem
        {
            Id = Guid.NewGuid().ToString(),
            PartitionKey = Constants.CosmosDbConfiguration.PartitonKey,
            IdempotencyKey = idempotencyKey,
            CreationTime = DateTime.UtcNow,
            Value = value,
            Type = type,
        };
    }
}
