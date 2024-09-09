using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;

namespace HE.FMS.Middleware.Reclaims.Functions;

public class ProcessCreateReclaim : DataExportFunctionBase
{
    public ProcessCreateReclaim(
        IDbItemClient dbItemClient,
        ICsvFileWriter csvFileWriter)
        : base(dbItemClient, csvFileWriter)
    {
    }

    [Function("ProcessCreateReclaim")]
    public async Task Run(
        [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        CancellationToken cancellationToken)
    {
        await Process(CosmosDbItemType.Reclaim, cancellationToken);
    }
}
