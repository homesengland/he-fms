using HE.FMS.IntegrationPlatform.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;

namespace HE.FMS.IntegrationPlatform.Extensions;

public static class FunctionsWorkerApplicationBuilderExtensions
{
    public static IFunctionsWorkerApplicationBuilder UseFmsMiddlewares(this IFunctionsWorkerApplicationBuilder builder)
        => builder
            .UseMiddleware<ExceptionHandlingWithResponseMiddleware>()
            .UseMiddleware<ExceptionHandlingWithoutResponseMiddleware>()
            .UseMiddleware<ExceptionHandlingForServiceBusTriggersMiddleware>()
            .UseMiddleware<RequestLoggingMiddleware>();
}
