namespace HE.FMS.Middleware.Contract.Common.CosmosDb;
public class TraceItem : CosmosDataItem
{
    public static TraceItem CreateTraceItem(string partitionKey, object value, string idempotencyKey, CosmosDbItemType type)
    {
        return new TraceItem
        {
            Id = Guid.NewGuid().ToString(),
            PartitionKey = partitionKey,
            IdempotencyKey = idempotencyKey,
            CreationTime = DateTime.UtcNow,
            Value = value,
            Type = type,
        };
    }
}
