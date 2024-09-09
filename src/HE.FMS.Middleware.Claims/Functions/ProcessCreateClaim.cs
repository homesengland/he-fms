using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Providers.Efin;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;

namespace HE.FMS.Middleware.Claims.Functions;

public class ProcessCreateClaim : DataExportFunctionBase
{
    private readonly IClaimConverter _claimConverter;
    private readonly ICsvFileGenerator _csvFileGenerator;

    public ProcessCreateClaim(
        IDbItemClient dbItemClient,
        ICsvFileWriter csvFileWriter,
        IClaimConverter claimConverter,
        ICsvFileGenerator csvFileGenerator)
        : base(dbItemClient, csvFileWriter)
    {
        _claimConverter = claimConverter;
        _csvFileGenerator = csvFileGenerator;
    }

    [Function("ProcessCreateClaim")]
    public async Task Run(
        [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        CancellationToken cancellationToken)
    {
        await Process(CosmosDbItemType.Claim, cancellationToken);
    }

    protected override IEnumerable<BlobData> Convert(IEnumerable<CosmosDbItem> items)
    {
        var blobData = new List<BlobData>();

        var claims = items.Where(x => x.Value is ClaimPaymentRequest)
            .Select(x => x.Value as ClaimPaymentRequest).WhereNotNull();

        if (claims.IsNullOrEmpty())
        {
            throw new ArgumentException(nameof(claims));
        }

        var convertedData = _claimConverter.Convert(claims);

        blobData.Add(_csvFileGenerator.GenerateFile(convertedData.CLCLB_Batch.AsEnumerable()));
        blobData.Add(_csvFileGenerator.GenerateFile(convertedData.CLI_Invoices));
        blobData.Add(_csvFileGenerator.GenerateFile(convertedData.CLA_InvoiceAnalyses));

        return blobData;
    }
}
