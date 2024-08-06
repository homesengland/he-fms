using System.Net;
using HE.FMS.Middleware.BusinessLogic.Mambu;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Functions.HttpTriggers.Mambu;

public class MambuRotateApiKeyHttpTrigger
{
    private readonly IMambuApiKeyService _mambuService;

    public MambuRotateApiKeyHttpTrigger(IMambuApiKeyService mambuService)
    {
        _mambuService = mambuService;
    }

    [Function(nameof(MambuRotateApiKeyHttpTrigger))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "mambu/rotate-api-key")]
        HttpRequestData req,
        FunctionContext executionContext,
        CancellationToken cancellationToken)
    {
        await _mambuService.RotateApiKey(cancellationToken);

        return req.CreateResponse(HttpStatusCode.NoContent);
    }
}
