using System.Net;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Functions.HttpTriggers.Grants;

public class OpenNewGrantAccountHttpTrigger
{
    private readonly IStreamSerializer _serializer;

    public OpenNewGrantAccountHttpTrigger(IStreamSerializer serializer)
    {
        _serializer = serializer;
    }

    [Function(nameof(OpenNewGrantAccountHttpTrigger))]
    public async Task<OpenNewGrantAccountTriggerResponse> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "grants")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        var dto = await _serializer.Deserialize<OpenNewGrantAccountRequest>(request.Body, cancellationToken);

        return new OpenNewGrantAccountTriggerResponse()
        {
            HttpResponse = request.CreateResponse(HttpStatusCode.Accepted),
            ServiceBusOutput = dto,
        };
    }

    public class OpenNewGrantAccountTriggerResponse
    {
        public HttpResponseData HttpResponse { get; set; }

        [ServiceBusOutput("%Grants:OpenGrantAccount:TopicName%", Connection = "ServiceBus:Connection")]
        public OpenNewGrantAccountRequest ServiceBusOutput { get; set; }
    }
}