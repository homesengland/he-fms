using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using HE.FMS.Middleware.Providers.File;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json.Linq;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Claims.Functions;

public class ProcessAndStoreClaimTimeTrigger : DataExportFunctionBase<ClaimItemSet>
{
    private readonly ICsvFileGenerator _csvFileGenerator;
    private readonly IEfinCosmosConfigClient _configurationClient;

    public ProcessAndStoreClaimTimeTrigger(
        IEfinCosmosClient efinCosmosDbClient,
        IFileWriter csvFileWriter,
        ICsvFileGenerator csvFileGenerator,
        IEfinCosmosConfigClient efinCosmosConfigClient)
        : base(
            efinCosmosDbClient,
            csvFileWriter)
    {
        _csvFileGenerator = csvFileGenerator;
        _configurationClient = efinCosmosConfigClient;
    }

    [Function(nameof(ProcessAndStoreClaimTimeTrigger))]
    public async Task Run(
        [TimerTrigger("%Claims:Create:CronExpression%")] TimerInfo myTimer,
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

        EfinConfigItem efinConfigItem;
        try
        {
            efinConfigItem = await _configurationClient.GetNextIndex(IndexConfiguration.Claim.BatchIndex, CosmosDbItemType.Claim);
        }
        catch (MissingConfigurationException)
        {
            await _configurationClient.CreateItem(
                IndexConfiguration.Reclaim.BatchIndex,
                CosmosDbItemType.Reclaim,
                IndexConfiguration.Reclaim.BatchIndexPrefix,
                IndexConfiguration.Reclaim.BatchIndexLength);

            efinConfigItem = await _configurationClient.GetNextIndex(IndexConfiguration.Claim.BatchIndex, CosmosDbItemType.Claim);
        }

        var batchRef = efinConfigItem.ToString();
        var batchNumber = efinConfigItem.IndexNumberToString();

        var itemSet = new ClaimItemSet
        {
            IdempotencyKey = items.First().IdempotencyKey,
            BatchNumber = batchNumber,
            CLCLB_Batch = CLCLB_Batch.Create(claims, batchRef),
        };

        foreach (var claimItem in claims)
        {
            claimItem.SetBatchRef(batchRef);
            itemSet.CLI_Invoices.Add(claimItem.CliInvoice);
            itemSet.CLA_InvoiceAnalyses.Add(claimItem.ClaInvoiceAnalysis);
        }

        return itemSet;
    }

    protected override IEnumerable<BlobData> PrepareFiles(ClaimItemSet convertedData) =>
        [
            _csvFileGenerator.GenerateFile(convertedData.CLCLB_Batch.AsEnumerable(), CLCLB_Batch.FileName, convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(convertedData.CLI_Invoices, CLI_Invoice.FileName, convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(convertedData.CLA_InvoiceAnalyses, CLA_InvoiceAnalysis.FileName, convertedData.BatchNumber),
        ];
}
