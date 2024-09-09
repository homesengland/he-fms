using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Providers.CosmosDb;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;

namespace HE.FMS.Middleware.Shared.Base;
public abstract class ClaimBase<T>
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly ICosmosDbClient _cosmosDbClient;
    private readonly TopicClient _topicClient;
    private readonly IObjectSerializer _objectSerializer;

    protected ClaimBase(
        IStreamSerializer streamSerializer,
        ICosmosDbClient cosmosDbClient,
        IObjectSerializer objectSerializer,
        TopicClient topicClient)
    {
        _streamSerializer = streamSerializer;
        _cosmosDbClient = cosmosDbClient;
        _objectSerializer = objectSerializer;
        _topicClient = topicClient;
    }

    protected async Task<HttpResponseData> Trigger(HttpRequestData request, CosmosDbItemType type, CancellationToken cancellationToken)
    {
        var inputData = await _streamSerializer.Deserialize<T>(request.Body, cancellationToken) ?? throw new InvalidRequestException($"Empty request body");

        var idempotencyKey = request.GetIdempotencyHeader();

        var cosmosDbOutput = CosmosDbItem.CreateCosmosDbItem(inputData, idempotencyKey, type);

        await _cosmosDbClient.UpsertItem(cosmosDbOutput, cancellationToken);

        var topicOutput = new Message(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(inputData)))
        {
            CorrelationId = idempotencyKey,
        };

        await _topicClient.SendAsync(topicOutput);

        return request.CreateResponse(HttpStatusCode.Accepted);
    }
}
