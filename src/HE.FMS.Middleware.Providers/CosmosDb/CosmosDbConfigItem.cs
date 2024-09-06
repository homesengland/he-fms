using System.Globalization;
using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Providers.CosmosDb;
public class CosmosDbConfigItem : ICosmosDbItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    public string PartitionKey { get; set; }

    public int Index { get; set; }

    public int IndexLength { get; set; }

    public CosmosDbItemType Type { get; set; }

    public string FieldName { get; set; }

    public string Prefix { get; set; }

    public static CosmosDbConfigItem Create(string partitionKey, CosmosDbItemType type, string fieldName, int indexLength, string prefix) => new()
    {
        Id = Guid.NewGuid().ToString(),
        PartitionKey = partitionKey,
        Type = type,
        FieldName = fieldName,
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
