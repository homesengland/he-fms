using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Shared.Tests;

public class ExceptionHandlingWithoutResponseMiddlewareTests
{
    private readonly TestLogger<ExceptionHandlingWithoutResponseMiddleware> _logger;
    private readonly ExceptionHandlingWithoutResponseMiddleware _middleware;
    private readonly FunctionContext _context;
    private readonly FunctionExecutionDelegate _next;

    public ExceptionHandlingWithoutResponseMiddlewareTests()
    {
        _logger = new TestLogger<ExceptionHandlingWithoutResponseMiddleware>();
        _middleware = new ExceptionHandlingWithoutResponseMiddleware(_logger);
        _context = Substitute.For<FunctionContext>();
        _next = Substitute.For<FunctionExecutionDelegate>();
    }

    [Fact]
    public async Task Invoke_ShouldLogError_WhenExceptionIsThrown_ForTimeTrigger()
    {
        // Arrange  
#pragma warning disable CA2201
        var exception = new Exception("Test exception");
#pragma warning restore CA2201
        _next(Arg.Any<FunctionContext>()).Returns(Task.FromException(exception));
        SetupContextWithTriggerType(Constants.FunctionsTriggers.TimeTrigger);

        // Act  
        await _middleware.Invoke(_context, _next);

        // Assert  
        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Equal(exception, logEntry.Exception);
        Assert.Equal(exception.Message, logEntry.Message);
    }

    [Fact]
    public async Task Invoke_ShouldLogError_WhenExceptionIsThrown_ForOrchestrationTrigger()
    {
        // Arrange  
#pragma warning disable CA2201
        var exception = new Exception("Test exception");
#pragma warning restore CA2201
        _next(Arg.Any<FunctionContext>()).Returns(Task.FromException(exception));
        SetupContextWithTriggerType(Constants.FunctionsTriggers.OrchestrationTrigger);

        // Act  
        await _middleware.Invoke(_context, _next);

        // Assert  
        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Equal(exception, logEntry.Exception);
        Assert.Equal(exception.Message, logEntry.Message);
    }

    [Fact]
    public async Task Invoke_ShouldLogError_WhenExceptionIsThrown_ForActivityTrigger()
    {
        // Arrange  
#pragma warning disable CA2201
        var exception = new Exception("Test exception");
#pragma warning restore CA2201
        _next(Arg.Any<FunctionContext>()).Returns(Task.FromException(exception));
        SetupContextWithTriggerType(Constants.FunctionsTriggers.ActivityTrigger);

        // Act  
        await _middleware.Invoke(_context, _next);

        // Assert  
        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Error, logEntry.LogLevel);
        Assert.Equal(exception, logEntry.Exception);
        Assert.Equal(exception.Message, logEntry.Message);
    }

    [Fact]
    public async Task Invoke_ShouldNotLogError_WhenNoExceptionIsThrown()
    {
        // Arrange  
        _next(Arg.Any<FunctionContext>()).Returns(Task.CompletedTask);
        SetupContextWithTriggerType(Constants.FunctionsTriggers.TimeTrigger);

        // Act  
        await _middleware.Invoke(_context, _next);

        // Assert  
        Assert.Empty(_logger.LogEntries);
    }

    [Fact]
    public async Task Invoke_ShouldNotLogError_WhenTriggerUnknownTrigger()
    {
        // Arrange  
#pragma warning disable CA2201
        _next(Arg.Any<FunctionContext>()).Returns(Task.FromException(new Exception("Test exception")));
#pragma warning restore CA2201
        SetupContextWithTriggerType("UnknownTrigger");

        // Assert  
        Assert.Empty(_logger.LogEntries);
        await Assert.ThrowsAsync<Exception>(async () => await _middleware.Invoke(_context, _next));
    }

    private void SetupContextWithTriggerType(string triggerType)
    {
        // Create a mock binding metadata  
        var bindingMetadata = Substitute.For<BindingMetadata>();
        bindingMetadata.Type.Returns(triggerType);

        // Create a mock function definition  
        var functionDefinition = Substitute.For<FunctionDefinition>();
        functionDefinition.InputBindings.Values.Returns([bindingMetadata]);

        // Set the function definition on the context  
        _context.FunctionDefinition.Returns(functionDefinition);
    }
}
