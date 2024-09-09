using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Providers.Efin;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;

namespace HE.FMS.Middleware.Reclaims.Functions;

public class ProcessCreateReclaim : DataExportFunctionBase
{
    private readonly IReclaimConverter _reclaimConverter;
    private readonly ICsvFileGenerator _csvFileGenerator;

    public ProcessCreateReclaim(
        IDbItemClient dbItemClient,
        ICsvFileWriter csvFileWriter,
        IReclaimConverter reclaimConverter,
        ICsvFileGenerator csvFileGenerator)
        : base(dbItemClient, csvFileWriter)
    {
        _reclaimConverter = reclaimConverter;
        _csvFileGenerator = csvFileGenerator;
    }

    [Function("ProcessCreateReclaim")]
    public async Task Run(
        [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        CancellationToken cancellationToken)
    {
        await Process(CosmosDbItemType.Reclaim, cancellationToken);
    }

    protected override IEnumerable<BlobData> Convert(IEnumerable<CosmosDbItem> items)
    {
        var blobData = new List<BlobData>();

        var claims = items.Where(x => x.Value is ReclaimPaymentRequest)
            .Select(x => x.Value as ReclaimPaymentRequest).WhereNotNull();

        if (claims.IsNullOrEmpty())
        {
            throw new ArgumentException(nameof(claims));
        }

        var convertedData = _reclaimConverter.Convert(claims);

        blobData.Add(_csvFileGenerator.GenerateFile(convertedData.CLI_IW_BAT.AsEnumerable()));
        blobData.Add(_csvFileGenerator.GenerateFile(convertedData.CLI_IW_ILTes));
        blobData.Add(_csvFileGenerator.GenerateFile(convertedData.CLI_IW_INAes));
        blobData.Add(_csvFileGenerator.GenerateFile(convertedData.CLI_IW_INLes));
        blobData.Add(_csvFileGenerator.GenerateFile(convertedData.CLI_IW_INVes));
        blobData.Add(_csvFileGenerator.GenerateFile(convertedData.CLI_IW_ITLes));

        return blobData;
    }
}
