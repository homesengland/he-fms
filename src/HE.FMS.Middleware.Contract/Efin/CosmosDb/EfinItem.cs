using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Contract.Common.CosmosDb;

namespace HE.FMS.Middleware.Contract.Efin.CosmosDb;
public class EfinItem : CosmosDataItem
{

    public CosmosDbItemStatus Status { get; set; } = CosmosDbItemStatus.NotProcessed;

    public static EfinItem CreateEfinItem(string partitionKey, object value, string idempotencyKey, CosmosDbItemType type)
    {
        return new EfinItem
        {
            Id = Guid.NewGuid().ToString(),
            PartitionKey = partitionKey,
            IdempotencyKey = idempotencyKey,
            CreationTime = DateTime.UtcNow,
            Value = value,
            Type = type,
            Status = CosmosDbItemStatus.NotProcessed,
        };
    }
}
