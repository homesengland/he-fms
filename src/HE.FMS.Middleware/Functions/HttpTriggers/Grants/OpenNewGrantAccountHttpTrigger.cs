using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;

namespace HE.FMS.Middleware.Functions.HttpTriggers.Grants;

public class OpenNewGrantAccountHttpTrigger
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly IObjectSerializer _objectSerializer;
    private readonly TopicClient _topicClient;
    private readonly ITraceCosmosClient _cosmosDbClient;

    public OpenNewGrantAccountHttpTrigger(
        IStreamSerializer streamSerializer,
        IObjectSerializer objectSerializer,
        ITopicClientFactory topicClientFactory,
        ITraceCosmosClient cosmosDbClient)
    {
        _streamSerializer = streamSerializer;
        _objectSerializer = objectSerializer;
        _cosmosDbClient = cosmosDbClient;
        _topicClient = topicClientFactory.GetTopicClient("Grants:OpenGrantAccount:TopicName");
    }

    [Function(nameof(OpenNewGrantAccountHttpTrigger))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "grants")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        var inputData = await _streamSerializer.Deserialize<OpenNewGrantAccountRequest>(request.Body, cancellationToken);

        var idempotencyKey = request.GetIdempotencyHeader();

        var cosmosDbOutput = TraceItem.CreateTraceItem(Constants.CosmosDbConfiguration.PartitonKey, inputData, idempotencyKey, string.Empty, CosmosDbItemType.Grant);

        await _cosmosDbClient.UpsertItem(cosmosDbOutput, cancellationToken);

        var topicOutput = new Message(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(inputData)))
        {
            CorrelationId = idempotencyKey,
        };

        await _topicClient.SendAsync(topicOutput);

        return request.CreateResponse(HttpStatusCode.Accepted);
    }
}
