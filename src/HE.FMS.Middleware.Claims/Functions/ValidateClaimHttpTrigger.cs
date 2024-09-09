using System;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.ServiceBus;
using HE.FMS.Middleware.Shared.Base;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Claims.Functions;
public class ValidateClaimHttpTrigger : ClaimBase<ClaimPaymentRequest>
{
    public ValidateClaimHttpTrigger(
        IStreamSerializer streamSerializer,
        ICosmosDbClient cosmosDbClient,
        IObjectSerializer objectSerializer,
        ITopicClientFactory topicClientFactory)
        : base(streamSerializer, cosmosDbClient, objectSerializer, topicClientFactory.GetTopicClient("Claims:Create:TopicName"))
    {
    }

    [Function(nameof(ValidateClaimHttpTrigger))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "claims")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await Trigger(request, CosmosDbItemType.Claim, cancellationToken);
        }
        catch (AggregateException)
        {
            throw new AggregateException(ConstantExceptionMessage.ClaimsValidationException);
        }
    }
}
