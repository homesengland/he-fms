using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Providers.CosmosDb.Base;
public class CosmosItem : ICosmosItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    public string PartitionKey { get; set; }

    public string IdempotencyKey { get; set; }

    public DateTimeOffset? CreationTime { get; set; }

    public object Value { get; set; }

    public CosmosDbItemType Type { get; set; }
}
