using System.Globalization;
using System.Text.Json.Serialization;
using HE.FMS.Middleware.Providers.CosmosDb.Base;

namespace HE.FMS.Middleware.Providers.CosmosDb;
public class CosmosDbConfigItem : IDbItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    public string PartitionKey { get; set; }

    public int Index { get; set; }

    public int IndexLength { get; set; }

    public CosmosDbItemType Type { get; set; }

    public string IndexName { get; set; }

    public string Prefix { get; set; }

    public static CosmosDbConfigItem Create(string partitionKey, CosmosDbItemType type, string indexName, int indexLength, string prefix) => new()
    {
        Id = Guid.NewGuid().ToString(),
        PartitionKey = partitionKey,
        Type = type,
        IndexName = indexName,
        Index = 0,
        IndexLength = indexLength,
        Prefix = prefix,
    };

    public string GetNextIndex()
    {
        Index += 1;
        return Prefix + Index.ToString(CultureInfo.InvariantCulture).PadLeft(IndexLength, '0');
    }
}
