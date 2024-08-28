using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Functions.Base;
using HE.FMS.Middleware.Middlewares;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Functions.HttpTriggers.Claims;
public class CreateClaimHttpTrigger : ClaimBase<ClaimPaymentRequest>
{
    public CreateClaimHttpTrigger(
        IStreamSerializer streamSerializer,
        IObjectSerializer objectSerializer,
        ITopicClientFactory topicClientFactory,
        CosmosDbHelper cosmosDbHelper)
        : base(streamSerializer, objectSerializer, topicClientFactory, cosmosDbHelper, "Claims:Create:TopicName")
    {
    }

    [Function(nameof(CreateClaimHttpTrigger))]
    public async Task<ClaimBaseResponse> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "claims")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await Trigger(request, cancellationToken);
        }
        catch (AggregateException)
        {
            throw new AggregateException(ConstantExceptionMessage.ClaimsValidationException);
        }
    }
}
