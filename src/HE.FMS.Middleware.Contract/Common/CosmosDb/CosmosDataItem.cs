namespace HE.FMS.Middleware.Contract.Common.CosmosDb;
public class CosmosDataItem : CosmosBaseItem
{
    public DateTimeOffset? CreationTime { get; set; }

    public string IdempotencyKey { get; set; }

    public string Environment { get; set; }

    public CosmosDbItemType Type { get; set; }
}
