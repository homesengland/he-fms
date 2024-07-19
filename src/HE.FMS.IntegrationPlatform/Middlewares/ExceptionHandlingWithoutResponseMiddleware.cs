using System.Diagnostics.CodeAnalysis;
using HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;
using HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Polly.Timeout;

namespace HE.FMS.IntegrationPlatform.Middlewares;

internal sealed class ExceptionHandlingWithoutResponseMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<ExceptionHandlingWithoutResponseMiddleware> _logger;

    public ExceptionHandlingWithoutResponseMiddleware(ILogger<ExceptionHandlingWithoutResponseMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Catch all exceptions to log them")]
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (context.FunctionDefinition.InputBindings.Values
             .Any(binding =>
                    binding.Type.EndsWith(Constants.FunctionsTriggers.TimeTrigger, StringComparison.OrdinalIgnoreCase) ||
                    binding.Type.EndsWith(Constants.FunctionsTriggers.OrchestrationTrigger, StringComparison.OrdinalIgnoreCase) ||
                    binding.Type.EndsWith(Constants.FunctionsTriggers.ActivityTrigger, StringComparison.OrdinalIgnoreCase)))
        {
            try
            {
                await next(context);
            }
            catch (ExternalServerErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (CommunicationException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (TimeoutRejectedException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (FailedSerializationException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown exception");
            }
        }
        else
        {
            await next(context);
        }
    }
}
