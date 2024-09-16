using System.Globalization;
using System.Text.Json.Serialization;
using HE.FMS.Middleware.Providers.CosmosDb.Base;

namespace HE.FMS.Middleware.Providers.CosmosDb.Efin;
public class EfinConfigItem : ICosmosItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    public string PartitionKey { get; set; }

    public int Index { get; set; }

    public int IndexLength { get; set; }

    public CosmosDbItemType Type { get; set; }

    public string IndexName { get; set; }

    public string Prefix { get; set; }

    public static EfinConfigItem Create(string partitionKey, CosmosDbItemType type, string indexName, int indexLength, string prefix) => new()
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

    public override string ToString()
    {
        return Prefix + Index.ToString(CultureInfo.InvariantCulture).PadLeft(IndexLength, '0');
    }

    public string IndexNumberToString()
    {
        return Index.ToString(CultureInfo.InvariantCulture).PadLeft(IndexLength, '0');
    }
}
