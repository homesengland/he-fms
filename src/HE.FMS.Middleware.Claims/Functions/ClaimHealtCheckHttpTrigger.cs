using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HE.FMS.Middleware.Claims.Functions;
public class ClaimHealtCheckHttpTrigger
{
    private readonly HealthCheckService _healthCheck;

    public ClaimHealtCheckHttpTrigger(HealthCheckService healthCheck)
    {
        _healthCheck = healthCheck;
    }

    [Function(nameof(ClaimHealtCheckHttpTrigger))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req)
    {
        var status = await _healthCheck.CheckHealthAsync();

        return new OkObjectResult(Enum.GetName(typeof(HealthStatus), status.Status));
    }
}
