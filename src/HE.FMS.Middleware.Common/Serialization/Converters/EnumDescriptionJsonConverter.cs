using System.Text.Json;
using System.Text.Json.Serialization;
using HE.FMS.Middleware.Contract.Extensions;

namespace HE.FMS.Middleware.Common.Serialization.Converters;

public sealed class EnumDescriptionJsonConverter<T> : JsonConverter<T>
    where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString()?.GetEnumValue<T>() ?? default;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetEnumDescription());
    }
}
