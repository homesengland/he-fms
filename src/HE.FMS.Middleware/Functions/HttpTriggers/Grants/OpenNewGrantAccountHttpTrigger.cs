using System.Net;
using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using HE.FMS.Middleware.Providers.CosmosDb;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Functions.HttpTriggers.Grants;

public class OpenNewGrantAccountHttpTrigger
{
    private readonly IStreamSerializer _streamSerializer;

    public OpenNewGrantAccountHttpTrigger(IStreamSerializer streamSerializer)
    {
        _streamSerializer = streamSerializer;
    }

    [Function(nameof(OpenNewGrantAccountHttpTrigger))]
    public async Task<OpenNewGrantAccountTriggerResponse> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "grants")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        var dto = await _streamSerializer.Deserialize<OpenNewGrantAccountRequest>(request.Body, cancellationToken);

        var idempotencyKey = request.GetIdempotencyHeader();

        var cosmosDbOutput = new CosmosDbItem()
        {
            Id = idempotencyKey,
            PartitionKey = Constants.CosmosDBConfiguration.FMS,
            Value = dto,
        };

        return new OpenNewGrantAccountTriggerResponse()
        {
            HttpResponse = request.CreateResponse(HttpStatusCode.Accepted),
            ServiceBusOutput = dto,
            CosmosDbOutput = cosmosDbOutput,
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
