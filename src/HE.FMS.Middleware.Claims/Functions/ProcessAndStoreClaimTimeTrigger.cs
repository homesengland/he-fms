using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.BusinessLogic.Constants;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.File;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Claims.Functions;

public class ProcessAndStoreClaimTimeTrigger : DataExportFunctionBase<ClaimItemSet>
{
    private readonly ICsvFileGenerator _csvFileGenerator;
    private readonly IEfinIndexCosmosClient _indexClient;
    private readonly IClaimConverter _claimConverter;
    private readonly IEnvironmentValidator _environmentValidator;
    private readonly ILogger<ProcessAndStoreClaimTimeTrigger> _logger;

    public ProcessAndStoreClaimTimeTrigger(
        IEfinCosmosClient efinCosmosDbClient,
        IFileWriter csvFileWriter,
        ICsvFileGenerator csvFileGenerator,
        IEfinIndexCosmosClient indexClient,
        IClaimConverter claimConverter,
        IEnvironmentValidator environmentValidator,
        ILogger<ProcessAndStoreClaimTimeTrigger> logger)
        : base(
            efinCosmosDbClient,
            csvFileWriter)
    {
        _csvFileGenerator = csvFileGenerator;
        _indexClient = indexClient;
        _claimConverter = claimConverter;
        _environmentValidator = environmentValidator;
        _logger = logger;
    }

    [Function(nameof(ProcessAndStoreClaimTimeTrigger))]
    public async Task Run(
        [TimerTrigger("%Claims:Create:CronExpression%")] TimerInfo myTimer,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(ProcessAndStoreClaimTimeTrigger)} function started");

        var environments = _environmentValidator.GetAllowedEnvironments();

        foreach (var environment in environments)
        {
            await Process(CosmosDbItemType.Claim, environment, cancellationToken);
        }

        _logger.LogInformation($"{nameof(ProcessAndStoreClaimTimeTrigger)} function ended");
    }

    protected override async Task<ClaimItemSet> Convert(IEnumerable<EfinItem> items)
    {
        var claims = items.Select(x => ((JObject)x.Value).ToObject<ClaimItem>()).WhereNotNull();

        if (!claims.Any())
        {
            throw new ArgumentException(nameof(claims));
        }

        EfinIndexItem indexItem;
        try
        {
            indexItem = await _indexClient.GetNextIndex(IndexConfiguration.Claim.BatchIndex, CosmosDbItemType.Claim);
        }
        catch (MissingConfigurationException)
        {
            await _indexClient.CreateItem(
                IndexConfiguration.Claim.BatchIndex,
                CosmosDbItemType.Claim,
                IndexConfiguration.Claim.BatchIndexPrefix,
                IndexConfiguration.Claim.BatchIndexLength);

            indexItem = await _indexClient.GetNextIndex(IndexConfiguration.Claim.BatchIndex, CosmosDbItemType.Claim);
        }

        var batchRef = indexItem.GetCurrentId();
        var batchNumber = indexItem.GetCurrentIndex();

        var itemSet = new ClaimItemSet
        {
            IdempotencyKey = items.First().IdempotencyKey,
            BatchNumber = batchNumber,
            CLCLB_Batch = await _claimConverter.CreateBatch(claims, batchRef),
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
            _csvFileGenerator.GenerateFile(
                convertedData.CLCLB_Batch.AsEnumerable(),
                EfinConstants.FileNamePrefix.Claim.ClclbBatch,
                convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(
                convertedData.CLI_Invoices,
                EfinConstants.FileNamePrefix.Claim.CliInvoice,
                convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(
                convertedData.CLA_InvoiceAnalyses,
                EfinConstants.FileNamePrefix.Claim.ClaInvoiceAnalysis,
                convertedData.BatchNumber),
        ];
}
