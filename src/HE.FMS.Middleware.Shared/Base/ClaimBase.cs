using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;

namespace HE.FMS.Middleware.Shared.Base;
public class ClaimBase<T>
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly IObjectSerializer _objectSerializer;
    private readonly ICosmosDbClient _cosmosDbClient;
    private readonly TopicClient _topicClient;

    protected ClaimBase(
        IStreamSerializer streamSerializer,
        IObjectSerializer objectSerializer,
        ITopicClientFactory topicClientFactory,
        string topicName,
        ICosmosDbClient cosmosDbClient)
    {
        _streamSerializer = streamSerializer;
        _objectSerializer = objectSerializer;
        _cosmosDbClient = cosmosDbClient;
        _topicClient = topicClientFactory.GetTopicClient(topicName);
    }

    protected async Task<HttpResponseData> Trigger(HttpRequestData request, CancellationToken cancellationToken)
    {
        var inputData = await _streamSerializer.Deserialize<T>(request.Body, cancellationToken) ?? throw new InvalidRequestException($"Empty request body");

        var idempotencyKey = request.GetIdempotencyHeader();

        var cosmosDbOutput = CosmosDbItem.CreateCosmosDbItem(inputData, idempotencyKey);

        await _cosmosDbClient.UpsertItem(cosmosDbOutput, cancellationToken);

        var topicOutput = new Message(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(inputData))) { CorrelationId = idempotencyKey, };

        await _topicClient.SendAsync(topicOutput);

        return request.CreateResponse(HttpStatusCode.Accepted);
    }
}
