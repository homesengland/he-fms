using System.Net;
using System.Text;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Functions.HttpTriggers.Claims.Model;
using HE.FMS.Middleware.Middlewares;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;

namespace HE.FMS.Middleware.Functions.HttpTriggers.Claims;
public class CreateClaimHttpTrigger
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly IObjectSerializer _objectSerializer;
    private readonly CosmosDbHelper _cosmosDbHelper;
    private readonly TopicClient _topicClient;

    public CreateClaimHttpTrigger(
        IStreamSerializer streamSerializer,
        IObjectSerializer objectSerializer,
        ITopicClientFactory topicClientFactory,
        CosmosDbHelper cosmosDbHelper)
    {
        _streamSerializer = streamSerializer;
        _cosmosDbHelper = cosmosDbHelper;
        _objectSerializer = objectSerializer;
        _topicClient = topicClientFactory.GetTopicClient("Claims:Create:TopicName");
    }

    [Function(nameof(CreateClaimHttpTrigger))]
    public async Task<CreateClaimResponse> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "claims")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        try
        {
            var inputData = await _streamSerializer.Deserialize<CreateClaimRequest>(request.Body, cancellationToken);


            var cosmosDbOutput = _cosmosDbHelper.CreateCosmosDbItem(inputData, string.Empty);

            var idempotencyKey = request.GetIdempotencyHeader();

            var topicOutput = new Message(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(inputData))) { CorrelationId = idempotencyKey, };

            await _topicClient.SendAsync(topicOutput);

            return new CreateClaimResponse
            {
                HttpResponse = request.CreateResponse(HttpStatusCode.Accepted),
                CosmosDbOutput = cosmosDbOutput
            };
        }
        catch (AggregateException)
        {
            throw new AggregateException(ConstantExceptionMessage.ClaimsValidationException);
        }
    }
}
