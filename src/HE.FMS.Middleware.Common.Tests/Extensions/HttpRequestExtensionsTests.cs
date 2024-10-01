using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Tests.Fakes;
using Microsoft.Azure.Functions.Worker;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Extensions;
public class HttpRequestExtensionsTests
{
    [Fact]
    public void GetIdempotencyHeader_ShouldReturnHeaderValue_WhenHeaderIsPresent()
    {
        // Arrange
        var context = Substitute.For<FunctionContext>();
        var requestData = new FakeHttpRequestData(context);
        requestData.Headers.Add(Constants.CustomHeaders.IdempotencyKey, ["12345"]);

        // Act  
        var result = requestData.GetIdempotencyHeader();

        // Assert  
        Assert.Equal("12345", result);
    }

    [Fact]
    public void GetCustomHeader_ShouldReturnHeaderValue_WhenSingleHeaderIsPresent()
    {
        // Arrange  
        var context = Substitute.For<FunctionContext>();
        var requestData = new FakeHttpRequestData(context);
        requestData.Headers.Add("SomeHeader", ["12345"]);

        // Act  
        var result = requestData.GetCustomHeader("SomeHeader");

        // Assert  
        Assert.Equal("12345", result);
    }

    [Fact]
    public void GetCustomHeader_ShouldThrowInvalidRequestException_WhenMultipleHeadersArePresent()
    {
        // Arrange  
        var context = Substitute.For<FunctionContext>();
        var requestData = new FakeHttpRequestData(context);
        requestData.Headers.Add("SomeHeader", ["value1", "value2"]);

        // Act & Assert  
        var exception = Assert.Throws<InvalidRequestException>(() =>
            requestData.GetCustomHeader("SomeHeader"));

        Assert.Contains("Multiple 'SomeHeader' headers", exception.Message);

    }

    [Fact]
    public void GetCustomHeader_ShouldThrowMissingRequiredHeaderException_WhenHeaderIsNotPresent()
    {
        // Arrange  
        var context = Substitute.For<FunctionContext>();
        var requestData = new FakeHttpRequestData(context);
        requestData.Headers.Add("SomeHeader", [string.Empty]);

        // Act & Assert  
        var exception = Assert.Throws<MissingRequiredHeaderException>(() =>
            requestData.GetCustomHeader("NonExistentHeader"));

        Assert.Equal("MissingRequiredHeader", exception.Code);
    }
}
