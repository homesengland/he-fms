using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface ICosmosDbItem
{
    [JsonPropertyName("id")]
    string Id { get; }

    [JsonPropertyName("partitionKey")]
    string PartitionKey { get; }
}
