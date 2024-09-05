using Newtonsoft.Json;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface ICosmosDbItem
{
    [JsonProperty("id")]
    string Id { get; }

    [JsonProperty("partitionKey")]
    string PartitionKey { get; }
}
