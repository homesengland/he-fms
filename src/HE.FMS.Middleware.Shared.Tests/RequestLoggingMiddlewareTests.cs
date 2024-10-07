using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Shared.Tests;

public class RequestLoggingMiddlewareTests
{
    private readonly TestLogger<RequestLoggingMiddleware> _logger;
    private readonly RequestLoggingMiddleware _middleware;
    private readonly FunctionContext _context;
    private readonly FunctionExecutionDelegate _next;

    public RequestLoggingMiddlewareTests()
    {
        _logger = new TestLogger<RequestLoggingMiddleware>();
        _middleware = new RequestLoggingMiddleware(_logger);
        _context = Substitute.For<FunctionContext>();
        _next = Substitute.For<FunctionExecutionDelegate>();

        var loggerFactory = Substitute.For<ILoggerFactory>();
        loggerFactory.CreateLogger<RequestLoggingMiddleware>().Returns(_logger);

        _context.InstanceServices.GetService(typeof(ILoggerFactory)).Returns(loggerFactory);

        var functionDefinition = Substitute.For<FunctionDefinition>();
        functionDefinition.Name.Returns("TestFunction");
        _context.FunctionDefinition.Returns(functionDefinition);
    }

    [Fact]
    public async Task Invoke_ShouldLogFunctionName()
    {
        // Act  
        await _middleware.Invoke(_context, _next);

        // Assert  
        var logEntry = _logger.LogEntries.FirstOrDefault();
        Assert.NotNull(logEntry);
        Assert.Equal(LogLevel.Information, logEntry.LogLevel);
        Assert.Equal("Function TestFunction triggered", logEntry.Message);
        await _next.Received(1)(_context);
    }
}
