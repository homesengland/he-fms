using System.ComponentModel;
using System.Text.Json;
using FluentAssertions;
using HE.FMS.Middleware.Common.Serialization.Converters;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Serialization.Converters.EnumDescriptionJsonConverterTests;

public class ReadTests
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new EnumDescriptionJsonConverterFactory() },
    };

    private enum TestType
    {
        Undefined = 0,

        [Description("OPT1")]
        FirstOption,

        [Description("OPT2")]
        SecondOption,
    }

    [Fact]
    public void ShouldMapEnum_WhenEnumDescriptionIsUsed()
    {
        // given
        var json = $"{{\"value\":\"OPT1\"}}";

        // when
        var result = JsonSerializer.Deserialize<TestItem>(json, _serializerOptions);

        // then
        result.Should().NotBeNull();
        result!.Value.Should().Be(TestType.FirstOption);
    }

    [Fact]
    public void ShouldMapEnum_WhenEnumNameIsUsed()
    {
        // given
        var json = $"{{\"value\":\"SecondOption\"}}";

        // when
        var result = JsonSerializer.Deserialize<TestItem>(json, _serializerOptions);

        // then
        result.Should().NotBeNull();
        result!.Value.Should().Be(TestType.SecondOption);
    }

    [Fact]
    public void ShouldMapToDefault_WhenEnumValueIsNotDefined()
    {
        // given
        var json = $"{{\"value\":\"I do not know what is this option\"}}";

        // when
        var result = JsonSerializer.Deserialize<TestItem>(json, _serializerOptions);

        // then
        result.Should().NotBeNull();
        result!.Value.Should().Be(TestType.Undefined);
    }

    private sealed record TestItem(TestType Value);
}
