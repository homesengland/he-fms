using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using HE.FMS.Middleware.Common.Config;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Common.Serialization;
using Microsoft.Extensions.Logging;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Serialization;

public class StreamSerializerTests
{
    private readonly JsonSerializerOptions _serializerOptions = new();
    private readonly TestLogger<StreamSerializer> _logger = new();

    [Fact]
    public async Task Deserialize_ShouldReturnDeserializedObject_WhenValidJson()
    {
        // Arrange  
        var serializer = CreateStreamSerializer();
        var json = $"{{\"Name\": \"Test\"}}";
        var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
        var cancellationToken = CancellationToken.None;

        // Act  
        var result = await serializer.Deserialize<TestModel>(stream, cancellationToken);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task Deserialize_WithOptions_ShouldReturnDeserializedObject_WhenValidJson()
    {
        // Arrange  
        var serializer = CreateStreamSerializer();
        var json = $"{{\"Name\": \"Test\"}}";
        var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
        var cancellationToken = CancellationToken.None;

        // Act  
        var result = await serializer.Deserialize<TestModel>(stream, CommonModule.CommonSerializerOptions, cancellationToken);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task Deserialize_ShouldLogErrorAndThrowFailedSerializationException_WhenJsonExceptionOccurs()
    {
        // Arrange  
        var serializer = CreateStreamSerializer();
        var invalidJson = "{\"Name\": ";
        var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(invalidJson));
        var cancellationToken = CancellationToken.None;

        // Act & Assert  
        var exception = await Assert.ThrowsAsync<FailedSerializationException>(() =>
            serializer.Deserialize<TestModel>(stream, cancellationToken));

        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Equal("FailedSerialization", exception.Code);
    }

    [Fact]
    public async Task Deserialize_ShouldThrowInvalidRequestException_WhenValidationFails()
    {
        // Arrange  
        var serializer = CreateStreamSerializer();
        var json = $"{{\"Names\": \"Test\"}}";
        var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
        var cancellationToken = CancellationToken.None;

        // Act & Assert  
        var exception = await Assert.ThrowsAsync<InvalidRequestException>(() =>
            serializer.Deserialize<TestModel>(stream, cancellationToken));

        Assert.Contains("The Name field is required", exception.Message);
    }

    private StreamSerializer CreateStreamSerializer()
    {
        return new StreamSerializer(_serializerOptions, _logger);
    }

    // Dummy model class for testing  
    public class TestModel
    {
        [Required]
        public string Name { get; set; }
    }
}
