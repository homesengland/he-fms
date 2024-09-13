using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Shared.Middlewares;

internal sealed class RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger) : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        logger.LogInformation("Function {FunctionName} triggered", context.FunctionDefinition.Name);

        await next(context);
    }
}
