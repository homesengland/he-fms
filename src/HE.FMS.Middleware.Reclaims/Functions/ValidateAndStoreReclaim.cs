using System;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Shared.Base;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Reclaims.Functions;
public class ValidateAndStoreReclaim : ClaimBase<ReclaimPaymentRequest>
{
    public ValidateAndStoreReclaim(
        IStreamSerializer streamSerializer,
        ICosmosDbClient cosmosDbClient)
        : base(streamSerializer, cosmosDbClient)
    {
    }

    [Function(nameof(ValidateAndStoreReclaim))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "reclaims")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await Trigger(request, CosmosDbItemType.Reclaim, cancellationToken);
        }
        catch (AggregateException)
        {
            throw new AggregateException(ConstantExceptionMessage.ReclaimsValidationException);
        }
    }
}
