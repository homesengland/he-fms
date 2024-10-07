using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Shared.Tests;

public class ExceptionHandlingForServiceBusTriggersMiddlewareTests
{
    private readonly TestLogger<ExceptionHandlingForServiceBusTriggersMiddleware> _logger;
    private readonly ExceptionHandlingForServiceBusTriggersMiddleware _middleware;
    private readonly FunctionContext _context;
    private readonly FunctionExecutionDelegate _next;

    public ExceptionHandlingForServiceBusTriggersMiddlewareTests()
    {
        _logger = new TestLogger<ExceptionHandlingForServiceBusTriggersMiddleware>();
        _middleware = new ExceptionHandlingForServiceBusTriggersMiddleware(_logger);
        _context = Substitute.For<FunctionContext>();
        _next = Substitute.For<FunctionExecutionDelegate>();
    }

    [Fact]
    public async Task Invoke_ShouldLogErrorAndRethrow_WhenExceptionIsThrown_ForServiceBusTrigger()
    {
        // Arrange  
#pragma warning disable CA2201
        var exception = new Exception("Test exception");
#pragma warning restore CA2201
        _next(Arg.Any<FunctionContext>()).Returns(Task.FromException(exception));
        SetupContextWithTriggerType(Constants.FunctionsTriggers.ServiceBusTrigger);

        // Act & Assert  
        await Assert.ThrowsAsync<Exception>(() => _middleware.Invoke(_context, _next));
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
        SetupContextWithTriggerType(Constants.FunctionsTriggers.ServiceBusTrigger);

        // Act  
        await _middleware.Invoke(_context, _next);

        // Assert  
        Assert.Empty(_logger.LogEntries);
    }

    [Fact]
    public async Task Invoke_ShouldNotLogError_WhenTriggerIsNotServiceBus()
    {
        // Arrange  
#pragma warning disable CA2201
        var exception = new Exception("Test exception");
#pragma warning restore CA2201
        _next(Arg.Any<FunctionContext>()).Returns(Task.FromException(exception));
        SetupContextWithTriggerType("SomeOtherTrigger");

        // Act  
        var ex = await Record.ExceptionAsync(() => _middleware.Invoke(_context, _next));

        // Assert  
        Assert.Empty(_logger.LogEntries);
        Assert.Equal(exception, ex);
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
