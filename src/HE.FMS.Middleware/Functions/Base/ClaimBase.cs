using System.Net;
using System.Text;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;

namespace HE.FMS.Middleware.Functions.Base;
public class ClaimBase<T>
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly IObjectSerializer _objectSerializer;
    private readonly CosmosDbHelper _cosmosDbHelper;
    private readonly TopicClient _topicClient;

    protected ClaimBase(
        IStreamSerializer streamSerializer,
        IObjectSerializer objectSerializer,
        ITopicClientFactory topicClientFactory,
        CosmosDbHelper cosmosDbHelper,
        string topicName)
    {
        _streamSerializer = streamSerializer;
        _objectSerializer = objectSerializer;
        _cosmosDbHelper = cosmosDbHelper;
        _topicClient = topicClientFactory.GetTopicClient(topicName);
    }

    protected async Task<ClaimBaseResponse> Trigger(HttpRequestData request, CancellationToken cancellationToken)
    {
        var inputData = await _streamSerializer.Deserialize<T>(request.Body, cancellationToken);

        if (inputData is not null)
        {
            var idempotencyKey = request.GetIdempotencyHeader();

            var cosmosDbOutput = _cosmosDbHelper.CreateCosmosDbItem(inputData, idempotencyKey);

            var topicOutput = new Message(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(inputData))) { CorrelationId = idempotencyKey, };

            await _topicClient.SendAsync(topicOutput);

            return new ClaimBaseResponse { HttpResponse = request.CreateResponse(HttpStatusCode.Accepted), CosmosDbOutput = cosmosDbOutput, };
        }

        throw new InvalidRequestException($"Empty request body");
    }
}
