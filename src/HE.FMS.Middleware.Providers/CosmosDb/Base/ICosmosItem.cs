using Newtonsoft.Json;

namespace HE.FMS.Middleware.Providers.CosmosDb.Base;

public interface ICosmosItem
{
    [JsonProperty("id")]
    string Id { get; }

    [JsonProperty("partitionKey")]
    string PartitionKey { get; }
}
