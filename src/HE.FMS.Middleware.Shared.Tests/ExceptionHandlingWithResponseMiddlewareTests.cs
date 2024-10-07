using System.Net;
using Azure.Core.Serialization;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Exceptions.Communication;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Polly.Timeout;
using Xunit;

namespace HE.FMS.Middleware.Shared.Tests;

public class ExceptionHandlingWithResponseMiddlewareTests
{
    private readonly ExceptionHandlingWithResponseMiddleware _middleware;
    private readonly TestLogger<ExceptionHandlingWithResponseMiddleware> _logger;
    private readonly FunctionContext _context;
    private readonly FunctionExecutionDelegate _next;
    private readonly HttpResponseData _httpResponseData;

    public ExceptionHandlingWithResponseMiddlewareTests()
    {
        var testHost = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .Build();

        using var scope = testHost.Services.CreateScope();
        _logger = new TestLogger<ExceptionHandlingWithResponseMiddleware>();
        _middleware = new ExceptionHandlingWithResponseMiddleware(_logger);
        _context = Substitute.For<FunctionContext>();
        _next = Substitute.For<FunctionExecutionDelegate>();

        var httpRequestData = Substitute.For<HttpRequestData>(_context);
        _httpResponseData = Substitute.For<HttpResponseData>(_context);
        _httpResponseData.Body.ReturnsForAnyArgs(new MemoryStream());
        _httpResponseData.Headers.ReturnsForAnyArgs([]);
        httpRequestData.CreateResponse().ReturnsForAnyArgs(_httpResponseData);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(Options.Create(new WorkerOptions { Serializer = new JsonObjectSerializer() }));

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _context.InstanceServices.ReturnsForAnyArgs(serviceProvider);

        var inputBinding = Substitute.For<BindingMetadata>();
        inputBinding.Type.Returns("httpTrigger");

        var bindingMetadata = Substitute.For<BindingMetadata>();
        bindingMetadata.Type.Returns(Constants.FunctionsTriggers.HttpTrigger);
        _context.FunctionDefinition.InputBindings.Values.Returns([bindingMetadata]);

        var invocationResult = Substitute.For<InvocationResult>();
        _context.GetInvocationResult().Returns(invocationResult);

        httpRequestData.CreateResponse().Returns(_httpResponseData);
        _httpResponseData.StatusCode = HttpStatusCode.OK;  // Default value

#pragma warning disable CA2012
        _context.GetHttpRequestDataAsync()!.Returns(new ValueTask<HttpRequestData>(httpRequestData));
#pragma warning restore CA2012
        _context.GetInvocationResult().Returns(invocationResult);
    }

    [Fact]
    public async Task Invoke_ShouldLogAndSetBadRequestResponse_ForValidationException()
    {
        // Arrange
        var exception = new MissingRequiredFieldException("Field");
        _next.When(x => x(_context)).Do(_ => throw exception);

        // Act
        await _middleware.Invoke(_context, _next);

        // Assert  
        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Warning, logEntry.LogLevel);
        Assert.Equal(exception, logEntry.Exception);
        Assert.Equal(exception.Message, logEntry.Message);
        Assert.Equal(HttpStatusCode.BadRequest, _httpResponseData.StatusCode);
    }

    [Fact]
    public async Task Invoke_ShouldLogAndSetBadRequestResponse_ForAggregateException()
    {
        // Arrange
        var exception = new AggregateException("Aggregate exception occurred");
        _next.When(x => x(_context)).Do(_ => throw exception);

        // Act
        await _middleware.Invoke(_context, _next);

        // Assert  
        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Warning, logEntry.LogLevel);
        Assert.Equal(exception, logEntry.Exception);
        Assert.Equal(exception.Message, logEntry.Message);
        Assert.Equal(HttpStatusCode.BadRequest, _httpResponseData.StatusCode);
    }

    [Fact]
    public async Task Invoke_ShouldLogAndSetInternalServerErrorResponse_ForExternalSystemException()
    {
        // Arrange
        var exception = new ExternalSystemCommunicationException("Test", HttpStatusCode.BadRequest);
        _next.When(x => x(_context)).Do(_ => throw exception);

        // Act
        await _middleware.Invoke(_context, _next);

        // Assert  
        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Equal(exception, logEntry.Exception);
        Assert.Equal(exception.Message, logEntry.Message);
        Assert.Equal(HttpStatusCode.InternalServerError, _httpResponseData.StatusCode);
    }

    [Fact]
    public async Task Invoke_ShouldLogAndSetInternalServerErrorResponse_ForTimeoutRejectedException()
    {
        // Arrange
        var exception = new TimeoutRejectedException("Timeout occurred");
        _next.When(x => x(_context)).Do(_ => throw exception);

        // Act
        await _middleware.Invoke(_context, _next);

        // Assert  
        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Equal(exception, logEntry.Exception);
        Assert.Equal(exception.Message, logEntry.Message);
        Assert.Equal(HttpStatusCode.InternalServerError, _httpResponseData.StatusCode);
    }

    [Fact]
    public async Task Invoke_ShouldLogAndSetInternalServerErrorResponse_ForUnknownException()
    {
        // Arrange
#pragma warning disable CA2201 // Do not raise reserved exception types
        var exception = new Exception("Unknown error");
#pragma warning restore CA2201 // Do not raise reserved exception types
        _next.When(x => x(_context)).Do(_ => throw exception);

        // Act
        await _middleware.Invoke(_context, _next);

        // Assert  
        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Equal(exception, logEntry.Exception);
        Assert.Equal("Unknown exception", logEntry.Message);
        Assert.Equal(HttpStatusCode.InternalServerError, _httpResponseData.StatusCode);
    }

    [Fact]
    public async Task Invoke_ShouldCallNext_WhenNoException()
    {
        // Act
        await _middleware.Invoke(_context, _next);

        // Assert
        await _next.Received(1)(_context);
    }
}
