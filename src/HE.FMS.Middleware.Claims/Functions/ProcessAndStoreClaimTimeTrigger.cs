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

        string batchNumber;
        try
        {
            batchNumber = await _configurationClient.GetNextIndex(IndexConfiguration.Claim.BatchIndex, CosmosDbItemType.Claim);
        }
        catch (MissingConfigurationException)
        {
            await _configurationClient.CreateItem(
                IndexConfiguration.Claim.BatchIndex,
                CosmosDbItemType.Claim,
                IndexConfiguration.Claim.BatchIndexPrefix,
                IndexConfiguration.Claim.BatchIndexLength);

            batchNumber = await _configurationClient.GetNextIndex(IndexConfiguration.Claim.BatchIndex, CosmosDbItemType.Claim);
        }

        var itemSet = new ClaimItemSet
        {
            IdempotencyKey = items.First().IdempotencyKey,
            CLCLB_Batch = CLCLB_Batch.Create(claims, batchNumber),
        };

        foreach (var claimItem in claims)
        {
            claimItem.SetBatchIndex(batchNumber);
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
