using System.Net;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;

namespace HE.FMS.Middleware.Functions.HttpTriggers.Grants;

public class OpenNewGrantAccountHttpTrigger
{
    private readonly IStreamSerializer _serializer;

    public OpenNewGrantAccountHttpTrigger(IStreamSerializer serializer)
    {
        _serializer = serializer;
    }

    [Function(nameof(OpenNewGrantAccountHttpTrigger))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "grants")]
        HttpRequestData request,
        [DurableClient] DurableTaskClient durableClient,
        CancellationToken cancellationToken)
    {
        var dto = await _serializer.Deserialize<OpenNewGrantAccountRequest>(request.Body, cancellationToken);

        await durableClient.ScheduleNewOpenNewGrantAccountOrchestrationInstanceAsync(dto);

        return request.CreateResponse(HttpStatusCode.Accepted);
    }
}
