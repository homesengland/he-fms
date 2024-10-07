using System.Text.Json;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Common.Serialization;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Serialization;

public class ObjectSerializerTests
{
    private readonly JsonSerializerOptions _serializerOptions = new();

    [Fact]
    public void Serialize_ShouldReturnJsonString_WhenObjectIsValid()
    {
        // Arrange  
        var serializer = CreateObjectSerializer();
        var obj = new TestModel { Name = "Test" };

        // Act  
        var json = serializer.Serialize(obj);

        // Assert  
        Assert.NotNull(json);
        Assert.Contains($"\"Name\":\"Test\"", json);
    }

    [Fact]
    public void Deserialize_ShouldReturnObject_WhenJsonIsValid()
    {
        // Arrange  
        var serializer = CreateObjectSerializer();
        var json = $"{{\"Name\":\"Test\"}}";

        // Act  
        var obj = serializer.Deserialize<TestModel>(json);

        // Assert  
        Assert.NotNull(obj);
        Assert.Equal("Test", obj.Name);
    }

    [Fact]
    public void Deserialize_ShouldThrowArgumentNullException_WhenJsonIsNullOrEmpty()
    {
        // Arrange  
        var serializer = CreateObjectSerializer();
        string json = null!;

        // Act & Assert  
        Assert.Throws<ArgumentNullException>(() => serializer.Deserialize<TestModel>(json));

        // Test for empty string  
        json = string.Empty;
        Assert.Throws<ArgumentNullException>(() => serializer.Deserialize<TestModel>(json));
    }

    [Fact]
    public void Deserialize_ShouldThrowFailedSerializationException_WhenDeserializationFails()
    {
        // Arrange  
        var serializer = CreateObjectSerializer();
        var invalidJson = $"{{\"Name\": {{\"Name\" : \"test\"}}"; // Invalid json for TestModel  

        // Act & Assert  
        Assert.Throws<FailedSerializationException>(() => serializer.Deserialize<TestModel>(invalidJson));
    }

    private ObjectSerializer CreateObjectSerializer()
    {
        return new ObjectSerializer(_serializerOptions);
    }

    // Dummy model class for testing  
    public class TestModel
    {
        public string Name { get; set; }
    }
}
