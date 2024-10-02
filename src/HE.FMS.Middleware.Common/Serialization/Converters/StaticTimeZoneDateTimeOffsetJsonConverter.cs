using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Common.Serialization.Converters;
[ExcludeFromCodeCoverage]
public sealed class StaticTimeZoneDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
{
    private readonly TimeZoneInfo _timeZone;

    public StaticTimeZoneDateTimeOffsetJsonConverter(TimeZoneInfo timeZone)
    {
        _timeZone = timeZone;
    }

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateTimeOffset = reader.GetDateTimeOffset();

        return TimeZoneInfo.ConvertTime(dateTimeOffset, _timeZone);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        var ukTime = TimeZoneInfo.ConvertTime(value, _timeZone);
        writer.WriteStringValue(ukTime.ToString("o", CultureInfo.InvariantCulture));
    }
}
