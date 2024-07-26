using System.Net;
using HE.FMS.IntegrationPlatform.BusinessLogic.Mambu;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.IntegrationPlatform.Functions.HttpTriggers.Mambu;

public class MambuHealthCheckHttpTrigger
{
    private readonly IMambuApiKeyService _mambuService;

    public MambuHealthCheckHttpTrigger(IMambuApiKeyService mambuService)
    {
        _mambuService = mambuService;
    }

    [Function(nameof(MambuHealthCheckHttpTrigger))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "mambu/hc")]
        HttpRequestData req,
        FunctionContext executionContext,
        CancellationToken cancellationToken)
    {
        await _mambuService.HealthCheck(cancellationToken);

        return req.CreateResponse(HttpStatusCode.NoContent);
    }
}
