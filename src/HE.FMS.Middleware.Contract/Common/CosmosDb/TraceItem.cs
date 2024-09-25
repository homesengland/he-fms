namespace HE.FMS.Middleware.Contract.Common.CosmosDb;
public class TraceItem : CosmosDataItem
{
    public static TraceItem CreateTraceItem(string partitionKey, object value, string idempotencyKey, string environment, CosmosDbItemType type)
    {
        return new TraceItem
        {
            Id = Guid.NewGuid().ToString(),
            PartitionKey = string.IsNullOrWhiteSpace(environment) ? partitionKey : $"{partitionKey}-{environment}",
            IdempotencyKey = idempotencyKey,
            Environment = environment,
            CreationTime = DateTime.UtcNow,
            Value = value,
            Type = type,
        };
    }
}
