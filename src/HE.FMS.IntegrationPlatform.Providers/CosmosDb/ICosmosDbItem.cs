using Newtonsoft.Json;

namespace HE.FMS.IntegrationPlatform.Providers.CosmosDb;

public interface ICosmosDbItem
{
    [JsonProperty("id")]
    string Id { get; }

    [JsonProperty("partitionKey")]
    string PartitionKey { get; }
}
