using System;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Shared.Base;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Claims.Functions;
public class CreateClaimHttpTrigger : ClaimBase<ClaimPaymentRequest>
{
    public CreateClaimHttpTrigger(
        IStreamSerializer streamSerializer,
        ICosmosDbClient cosmosDbClient)
        : base(streamSerializer, cosmosDbClient)
    {
    }

    [Function(nameof(CreateClaimHttpTrigger))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "claims")]
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
