namespace HE.FMS.Middleware.Contract.Common.CosmosDb;
public class CosmosBaseItem : ICosmosItem
{
    public string Id { get; set; }

    public string PartitionKey { get; set; }

    public object Value { get; set; }
}
