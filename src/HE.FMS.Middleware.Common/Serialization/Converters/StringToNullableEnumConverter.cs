using System.Text.Json;
using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Common.Serialization.Converters;
public class StringToNullableEnumConverter<T> : JsonConverter<T>
    where T : struct, Enum
{
    private readonly JsonConverter<T> _converter;
    private readonly Type _underlyingType;

    public StringToNullableEnumConverter()
        : this(null)
    {
    }

    public StringToNullableEnumConverter(JsonSerializerOptions? options)
    {
        if (options != null)
        {
            _converter = (JsonConverter<T>)options.GetConverter(typeof(T));
        }

        _underlyingType = Nullable.GetUnderlyingType(typeof(T))!;
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(T).IsAssignableFrom(typeToConvert);
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (_converter != null)
        {
            return _converter.Read(ref reader, _underlyingType, options);
        }

        var value = reader.GetString();

        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        if (!Enum.TryParse(_underlyingType, value, true, out var result))
        {
            throw new JsonException($"Unable to convert \"{value}\" to Enum \"{_underlyingType}\".");
        }

        return (T)result;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
