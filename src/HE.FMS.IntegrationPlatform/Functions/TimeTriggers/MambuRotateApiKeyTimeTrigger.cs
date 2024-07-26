using HE.FMS.IntegrationPlatform.BusinessLogic.Mambu;
using Microsoft.Azure.Functions.Worker;

namespace HE.FMS.IntegrationPlatform.Functions.TimeTriggers;

public class MambuRotateApiKeyTimeTrigger
{
    private readonly IMambuApiKeyService _mambuService;

    public MambuRotateApiKeyTimeTrigger(IMambuApiKeyService mambuService)
    {
        _mambuService = mambuService;
    }

    [Function(nameof(MambuRotateApiKeyTimeTrigger))]
    public async Task Run(
        [TimerTrigger("%Mambu:RotateApiKeyTimeTrigger%")] TimerInfo timeTrigger,
        CancellationToken cancellationToken)
    {
        await _mambuService.RotateApiKey(cancellationToken);
    }
}
