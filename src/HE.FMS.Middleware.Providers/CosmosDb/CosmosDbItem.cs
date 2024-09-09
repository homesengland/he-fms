using System.Text.Json.Serialization;
using HE.FMS.Middleware.Common;

namespace HE.FMS.Middleware.Providers.CosmosDb;
public class CosmosDbItem : ICosmosDbItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    public string PartitionKey { get; set; }

    public string IdempotencyKey { get; set; }

    public DateTimeOffset? CreationTime { get; set; }

    public object Value { get; set; }

    public CosmosDbItemType Type { get; set; }

    public static CosmosDbItem CreateCosmosDbItem(object value, string idempotencyKey, CosmosDbItemType type)
    {
        return new CosmosDbItem
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
