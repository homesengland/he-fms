﻿using System.Diagnostics.CodeAnalysis;
using System.Net;
using HE.FMS.IntegrationPlatform.Common.Exceptions;
using HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;
using HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Polly.Timeout;

namespace HE.FMS.IntegrationPlatform.Middlewares;

internal sealed class ExceptionHandlingWithResponseMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<ExceptionHandlingWithResponseMiddleware> _logger;

    public ExceptionHandlingWithResponseMiddleware(ILogger<ExceptionHandlingWithResponseMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Catch all exceptions to log them")]
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (context.FunctionDefinition.InputBindings.Values
             .Any(binding => binding.Type.EndsWith(Constants.FunctionsTriggers.HttpTrigger, StringComparison.OrdinalIgnoreCase)))
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                await SetErrorResponse(context, HttpStatusCode.BadRequest, ex);
            }
            catch (ExternalServerErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                await SetErrorResponse(context, ex.Code, errorCode: "ExternalError");
            }
            catch (CommunicationException ex)
            {
                _logger.LogError(ex, ex.Message);
                await SetErrorResponse(context, ex.Code, ex.Message);
            }
            catch (TimeoutRejectedException ex)
            {
                _logger.LogError(ex, ex.Message);
                await SetErrorResponse(context, HttpStatusCode.InternalServerError, errorCode: "ExternalError", errorMessage: "External service takes too long to respond.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown exception");
                await SetErrorResponse(context, HttpStatusCode.InternalServerError, errorCode: HttpStatusCode.InternalServerError.ToString());
            }
        }
        else
        {
            await next(context);
        }
    }

    private static async Task SetErrorResponse(FunctionContext context, HttpStatusCode httpStatusCode, ValidationException exception) =>
        await SetErrorResponse(context, httpStatusCode, exception.Code, exception.Message);

    private static async Task SetErrorResponse(FunctionContext context, HttpStatusCode httpStatusCode, string? errorCode = null, string? errorMessage = null)
    {
        var request = await context.GetHttpRequestDataAsync();
        if (request != null)
        {
            var response = request.CreateResponse(httpStatusCode);

            if (errorCode != null)
            {
                await response.WriteAsJsonAsync(new ErrorDto(errorCode, errorMessage), httpStatusCode);
            }

            context.GetInvocationResult().Value = response;
        }
    }
}
