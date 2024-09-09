using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Providers.CosmosDb.Base;

namespace HE.FMS.Middleware.Providers.CosmosDb.Efin;
public class EfinItem : DbItem
{
    public CosmosDbItemStatus Status { get; set; } = CosmosDbItemStatus.New;

    public static EfinItem CreateEfinItem(object value, string idempotencyKey, CosmosDbItemType type)
    {
        return new EfinItem
        {
            Id = Guid.NewGuid().ToString(),
            PartitionKey = Constants.CosmosDbConfiguration.PartitonKey,
            IdempotencyKey = idempotencyKey,
            CreationTime = DateTime.UtcNow,
            Value = value,
            Type = type,
            Status = CosmosDbItemStatus.New,
        };
    }
}
