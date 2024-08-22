using HE.FMS.Middleware.Providers.CosmosDb;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Functions.HttpTriggers.Claims.Model;
public class CreateClaimResponse
{
    public HttpResponseData HttpResponse { get; set; }

    [CosmosDBOutput("%CosmosDb:DatabaseId%", "%CosmosDb:ContainerId%", Connection = "CosmosDb:ConnectionString")]
    public CosmosDbItem CosmosDbOutput { get; set; }
}
