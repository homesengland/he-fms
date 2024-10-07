using System.Diagnostics.CodeAnalysis;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;

namespace HE.FMS.Middleware.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class FunctionsWorkerApplicationBuilderExtensions
{
    public static IFunctionsWorkerApplicationBuilder UseFmsMiddlewares(this IFunctionsWorkerApplicationBuilder builder)
        => builder
            .UseMiddleware<ExceptionHandlingWithResponseMiddleware>()
            .UseMiddleware<ExceptionHandlingWithoutResponseMiddleware>()
            .UseMiddleware<ExceptionHandlingForServiceBusTriggersMiddleware>()
            .UseMiddleware<RequestLoggingMiddleware>();
}
