using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Middlewares;

internal sealed class RequestLoggingMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var logger = context.GetLogger<RequestLoggingMiddleware>();
        logger.LogInformation("Function {FunctionName} triggered", context.FunctionDefinition.Name);

        await next(context);
    }
}
