using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.BusinessLogic.Mambu;
using Microsoft.Azure.Functions.Worker;

namespace HE.FMS.Middleware.Functions.TimeTriggers;

public class MambuRotateApiKeyTimeTrigger
{
    private readonly IMambuApiKeyService _mambuService;

    public MambuRotateApiKeyTimeTrigger(IMambuApiKeyService mambuService)
    {
        _mambuService = mambuService;
    }

    [Function(nameof(MambuRotateApiKeyTimeTrigger))]
    public async Task Run(
        [TimerTrigger("%Mambu:RotateApiKey:CronExpression%")] TimerInfo timeTrigger,
        CancellationToken cancellationToken)
    {
        await _mambuService.RotateApiKey(cancellationToken);
    }
}
