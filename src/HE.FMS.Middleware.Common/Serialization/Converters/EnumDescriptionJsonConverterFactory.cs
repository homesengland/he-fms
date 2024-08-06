using System.Text.Json;
using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Common.Serialization.Converters;

public sealed class EnumDescriptionJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum;

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
        (JsonConverter)Activator.CreateInstance(typeof(EnumDescriptionJsonConverter<>).MakeGenericType(typeToConvert))!;
}
