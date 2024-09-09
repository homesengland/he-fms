using System.Net;
using System.Threading;
using System.Threading.Tasks;
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "mambu/rotate-api-key")]
        HttpRequestData req,
        CancellationToken cancellationToken)
    {
        await _mambuService.RotateApiKey(cancellationToken);

        return req.CreateResponse(HttpStatusCode.NoContent);
    }
}
