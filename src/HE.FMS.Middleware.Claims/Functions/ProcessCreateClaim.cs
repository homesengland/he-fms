using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Claims.Functions;

public class ProcessCreateClaim
{
    [Function("ProcessCreateClaim")]
    public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
    {
        // TODO: Get Claims from Cosmos and write to SCV
    }
}
