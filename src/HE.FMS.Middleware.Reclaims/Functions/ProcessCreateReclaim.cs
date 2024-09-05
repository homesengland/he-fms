using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Reclaims.Functions;

public class ProcessCreateReclaim
{
    [Function("ProcessCreateReclaim")]
    public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
    {
        // TODO: Get Reclaims from Cosmos and write to SCV
    }
}
