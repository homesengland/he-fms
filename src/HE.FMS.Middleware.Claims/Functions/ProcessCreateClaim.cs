using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;

namespace HE.FMS.Middleware.Claims.Functions;

public class ProcessCreateClaim : DataExportFunctionBase
{
    public ProcessCreateClaim(
        IDbItemClient dbItemClient,
        ICsvFileWriter csvFileWriter)
        : base(dbItemClient, csvFileWriter)
    {
    }

    [Function("ProcessCreateClaim")]
    public async Task Run(
        [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        CancellationToken cancellationToken)
    {
        await Process(CosmosDbItemType.Claim, cancellationToken);
    }
}
