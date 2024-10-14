using System.Diagnostics.CodeAnalysis;
using HE.FMS.Middleware.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Shared.Middlewares;

internal sealed class ExceptionHandlingForServiceBusTriggersMiddleware(ILogger<ExceptionHandlingForServiceBusTriggersMiddleware> logger)
    : IFunctionsWorkerMiddleware
{
    private readonly ILogger<ExceptionHandlingForServiceBusTriggersMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Catch all exceptions to log them")]
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (context.FunctionDefinition.InputBindings.Values
             .Any(binding =>
                    binding.Type.EndsWith(Constants.FunctionsTriggers.ServiceBusTrigger, StringComparison.OrdinalIgnoreCase)))
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        else
        {
            await next(context);
        }
    }
}
