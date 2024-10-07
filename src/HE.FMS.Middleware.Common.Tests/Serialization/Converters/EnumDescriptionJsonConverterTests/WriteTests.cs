using System.ComponentModel;
using System.Text.Json;
using FluentAssertions;
using HE.FMS.Middleware.Common.Serialization.Converters;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Serialization.Converters.EnumDescriptionJsonConverterTests;

public class WriteTests
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new EnumDescriptionJsonConverterFactory() },
    };

    private enum TestType
    {
        [Description("OPT1")]
        FirstOption,

        SecondOption,
    }

    [Fact]
    public void ShouldWriteEnumDescription_WhenValueHasDescription()
    {
        // given && when
        var result = JsonSerializer.Serialize(new { Value = TestType.FirstOption }, _serializerOptions);

        // then
        result.Should().Be(expected: $$"""{"value":"OPT1"}""");
    }

    [Fact]
    public void ShouldWriteEnumValue_WhenValueHasNoDescription()
    {
        // given && when
        var result = JsonSerializer.Serialize(new { Value = TestType.SecondOption }, _serializerOptions);

        // then
        result.Should().Be($"{{\"value\":\"SecondOption\"}}");
    }

    [Fact]
    public void ShouldIgnoreValue_WhenValueIsNull()
    {
        // given && when
        var result = JsonSerializer.Serialize(new { Value = (TestType?)null }, _serializerOptions);

        // then
        result.Should().Be($"{{\"value\":null}}");
    }
}
