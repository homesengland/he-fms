using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Domain.Mambu;

internal sealed class MambuService : IMambuService
{
    private readonly IMambuGroupApiClient _groupApiClient;

    private readonly ILogger<MambuService> _logger;

    public MambuService(IMambuGroupApiClient groupApiClient, ILogger<MambuService> logger)
    {
        _groupApiClient = groupApiClient;
        _logger = logger;
    }

    public async Task HealthCheck(CancellationToken cancellationToken)
    {
        var response = await _groupApiClient.GetAll(cancellationToken);

        _logger.LogInformation("Mambu service is healthy. Groups count: {GroupsCount}", response.Count);
    }
}
