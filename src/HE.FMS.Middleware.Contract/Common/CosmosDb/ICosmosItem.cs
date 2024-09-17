using Newtonsoft.Json;

namespace HE.FMS.Middleware.Contract.Common.CosmosDb;

public interface ICosmosItem
{
    [JsonProperty("id")]
    string Id { get; }

    [JsonProperty("partitionKey")]
    string PartitionKey { get; }
}
