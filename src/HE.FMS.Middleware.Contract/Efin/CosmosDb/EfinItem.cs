using HE.FMS.Middleware.Contract.Common.CosmosDb;

namespace HE.FMS.Middleware.Contract.Efin.CosmosDb;
public class EfinItem : CosmosDataItem
{
    public CosmosDbItemStatus Status { get; set; } = CosmosDbItemStatus.NotProcessed;

    public static EfinItem CreateEfinItem(string partitionKey, object value, string idempotencyKey, string environment, CosmosDbItemType type)
    {
        return new EfinItem
        {
            Id = Guid.NewGuid().ToString(),
            PartitionKey = string.IsNullOrWhiteSpace(environment) ? partitionKey : $"{partitionKey}-{environment}",
            IdempotencyKey = idempotencyKey,
            Environment = environment,
            CreationTime = DateTime.UtcNow,
            Value = value,
            Type = type,
            Status = CosmosDbItemStatus.NotProcessed,
        };
    }
}
