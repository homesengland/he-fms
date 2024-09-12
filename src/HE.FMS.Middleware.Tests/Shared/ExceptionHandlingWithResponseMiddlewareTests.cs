using System.Net;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Exceptions.Communication;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Polly.Timeout;
using Xunit;

namespace HE.FMS.Middleware.Tests.Shared;

public class ExceptionHandlingWithResponseMiddlewareTests
{
    private readonly ILogger<ExceptionHandlingWithResponseMiddleware> _logger;
    private readonly ExceptionHandlingWithResponseMiddleware _middleware;
    private readonly FunctionContext _context;
    private readonly FunctionExecutionDelegate _next;

    public ExceptionHandlingWithResponseMiddlewareTests()
    {
        _logger = Substitute.For<ILogger<ExceptionHandlingWithResponseMiddleware>>();
        _middleware = new ExceptionHandlingWithResponseMiddleware(_logger);
        _context = Substitute.For<FunctionContext>();
        _next = Substitute.For<FunctionExecutionDelegate>();
        var test = Substitute.For<BindingMetadata>();
        test.Type.Returns(Constants.FunctionsTriggers.HttpTrigger);
        _context.FunctionDefinition.InputBindings.Values.Returns(new List<BindingMetadata>()
        {
            test
        });
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
        _logger.Received(1).LogWarning(exception, exception.Message);
        await _context.Received(1).GetHttpRequestDataAsync();
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
        _logger.Received(1).LogWarning(exception, exception.Message);
        await _context.Received(1).GetHttpRequestDataAsync();
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
        _logger.Received(1).LogError(exception, exception.Message);
        await _context.Received(1).GetHttpRequestDataAsync();
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
        _logger.Received(1).LogError(exception, exception.Message);
        await _context.Received(1).GetHttpRequestDataAsync();
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
        _logger.Received(1).LogError(exception, "Unknown exception");
        await _context.Received(1).GetHttpRequestDataAsync();
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
