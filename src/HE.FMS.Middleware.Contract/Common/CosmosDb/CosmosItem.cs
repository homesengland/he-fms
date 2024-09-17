namespace HE.FMS.Middleware.Contract.Common.CosmosDb;
public class CosmosItem : ICosmosItem
{
    public string Id { get; set; }

    public string PartitionKey { get; set; }

    public string IdempotencyKey { get; set; }

    public DateTimeOffset? CreationTime { get; set; }

    public object Value { get; set; }

    public CosmosDbItemType Type { get; set; }
}
