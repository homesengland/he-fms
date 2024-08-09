using System.Net;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using HE.FMS.Middleware.Providers.CosmosDb;
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
        var dto = await _serializer.Deserialize<OpenNewGrantAccountRequest>(request.Body, cancellationToken) ?? throw new InvalidOperationException();

        var idempotencyKey = request.Headers.GetValues(Constants.HttpHeaders.IdempotencyKey).Single();

        return new OpenNewGrantAccountTriggerResponse()
        {
            HttpResponse = request.CreateResponse(HttpStatusCode.Accepted),
            ServiceBusOutput = dto,
            CosmosDbOutput = new CosmosDbItem() { Id = idempotencyKey, PartitionKey = Constants.CosmosDBConfiguration.FMS, Value = dto },
        };
    }

    public class OpenNewGrantAccountTriggerResponse
    {
        public HttpResponseData HttpResponse { get; set; }

        [ServiceBusOutput("%Grants:OpenGrantAccount:TopicName%", Connection = "ServiceBus:Connection")]
        public OpenNewGrantAccountRequest ServiceBusOutput { get; set; }

        [CosmosDBOutput("%CosmosDb:DatabaseId%", "%CosmosDb:ContainerId%", Connection = "CosmosDb:ConnectionString")]
        public CosmosDbItem CosmosDbOutput { get; set; }
    }
}
