using System;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.ServiceBus;
using HE.FMS.Middleware.Shared.Base;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Reclaims.Functions;
public class CreateReclaimHttpTrigger : ClaimBase<CreateReclaimRequest>
{
    public CreateReclaimHttpTrigger(
        IStreamSerializer streamSerializer,
        IObjectSerializer objectSerializer,
        ITopicClientFactory topicClientFactory,
        ICosmosDbClient cosmosDbClient)
        : base(streamSerializer, objectSerializer, topicClientFactory, "Reclaims:Create:TopicName", cosmosDbClient)
    {
    }

    [Function(nameof(CreateReclaimHttpTrigger))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "reclaims")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await Trigger(request, cancellationToken);
        }
        catch (AggregateException)
        {
            throw new AggregateException(ConstantExceptionMessage.ReclaimsValidationException);
        }
    }
}
