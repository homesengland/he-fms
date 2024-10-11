using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Claims.Functions;
public class ClaimHealthCheckHttpTrigger
{
    private readonly HealthCheckService _healthCheck;
    private readonly ILogger<ClaimHealthCheckHttpTrigger> _logger;

    public ClaimHealthCheckHttpTrigger(HealthCheckService healthCheck, ILogger<ClaimHealthCheckHttpTrigger> logger)
    {
        _healthCheck = healthCheck;
        _logger = logger;
    }

    [Function(nameof(ClaimHealthCheckHttpTrigger))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req)
    {
        var status = await _healthCheck.CheckHealthAsync();

        var message = $"Health check status: {status.Status}";
        _logger.LogInformation(message);

        return new OkObjectResult(Enum.GetName(typeof(HealthStatus), status.Status));
    }
}
