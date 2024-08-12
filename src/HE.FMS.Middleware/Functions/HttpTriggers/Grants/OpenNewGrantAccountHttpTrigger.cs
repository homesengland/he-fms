using System.Net;
using System.Text;
using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace HE.FMS.Middleware.Functions.HttpTriggers.Grants;

public class OpenNewGrantAccountHttpTrigger
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly IObjectSerializer _objectSerializer;
    private readonly TopicClient _topicClient;

    public OpenNewGrantAccountHttpTrigger(IStreamSerializer streamSerializer, IObjectSerializer objectSerializer, ITopicClientFactory topicClientFactory)
    {
        _streamSerializer = streamSerializer;
        _objectSerializer = objectSerializer;
        _topicClient = topicClientFactory.GetTopicClient("Grants:OpenGrantAccount:TopicName");
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

        var topicOutput = new Message(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(dto)))
        {
            CorrelationId = idempotencyKey,
        };

        await _topicClient.SendAsync(topicOutput);

        return new OpenNewGrantAccountTriggerResponse()
        {
            HttpResponse = request.CreateResponse(HttpStatusCode.Accepted),
            CosmosDbOutput = cosmosDbOutput,
        };
    }

    public class OpenNewGrantAccountTriggerResponse
    {
        public HttpResponseData HttpResponse { get; set; }

        [CosmosDBOutput("%CosmosDb:DatabaseId%", "%CosmosDb:ContainerId%", Connection = "CosmosDb:ConnectionString")]
        public CosmosDbItem CosmosDbOutput { get; set; }
    }
}
