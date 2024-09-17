using System;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
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
        ITraceCosmosClient traceCosmosDbClient,
        IObjectSerializer objectSerializer,
        ITopicClientFactory topicClientFactory)
        : base(
            streamSerializer,
            traceCosmosDbClient,
            objectSerializer,
            topicClientFactory.GetTopicClient(Constants.Settings.ServiceBus.ClaimsTopic))
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
