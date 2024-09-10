using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Providers.Efin;
using HE.FMS.Middleware.Providers.ServiceBus;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json.Linq;

namespace HE.FMS.Middleware.Claims.Functions;

public class ProcessAndStoreClaimTimeTrigger : DataExportFunctionBase<ClaimItemSet>
{
    private readonly ICsvFileGenerator _csvFileGenerator;

    public ProcessAndStoreClaimTimeTrigger(
        IEfinCosmosClient efinCosmosDbClient,
        ICsvFileWriter csvFileWriter,
        ICsvFileGenerator csvFileGenerator,
        ITopicClientFactory topicClientFactory,
        IObjectSerializer objectSerializer)
        : base(
            efinCosmosDbClient,
            csvFileWriter)
    {
        _csvFileGenerator = csvFileGenerator;
    }

    [Function(nameof(ProcessAndStoreClaimTimeTrigger))]
    public async Task Run(
        [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        CancellationToken cancellationToken)
    {
        await Process(CosmosDbItemType.Claim, cancellationToken);
    }

    protected override async Task<ClaimItemSet> Convert(IEnumerable<EfinItem> items)
    {
        var claims = items.Select(x => ((JObject)x.Value).ToObject<ClaimItem>()).WhereNotNull();

        if (!claims.Any())
        {
            throw new ArgumentException(nameof(claims));
        }

        var itemSet = new ClaimItemSet
        {
            IdempotencyKey = items.First().IdempotencyKey,
            CLCLB_Batch = CLCLB_Batch.Create(claims),
        };

        foreach (var claimItem in claims)
        {
            itemSet.CLI_Invoices.Add(claimItem.CliInvoice);
            itemSet.CLA_InvoiceAnalyses.Add(claimItem.ClaInvoiceAnalysis);
        }

        return itemSet;
    }

    protected override IEnumerable<BlobData> PrepareFiles(ClaimItemSet convertedData) =>
        [
            _csvFileGenerator.GenerateFile(convertedData.CLCLB_Batch.AsEnumerable()),
            _csvFileGenerator.GenerateFile(convertedData.CLI_Invoices),
            _csvFileGenerator.GenerateFile(convertedData.CLA_InvoiceAnalyses),
        ];
}
