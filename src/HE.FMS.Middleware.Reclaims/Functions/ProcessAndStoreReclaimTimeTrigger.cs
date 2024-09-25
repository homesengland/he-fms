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
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.File;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json.Linq;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Reclaims.Functions;

public class ProcessAndStoreReclaimTimeTrigger : DataExportFunctionBase<ReclaimItemSet>
{
    private readonly ICsvFileGenerator _csvFileGenerator;
    private readonly IEfinIndexCosmosClient _indexClient;
    private readonly IReclaimConverter _reclaimConverter;
    private readonly IEnvironmentValidator _environmentValidator;

    public ProcessAndStoreReclaimTimeTrigger(
        IEfinCosmosClient efinCosmosDbClient,
        IFileWriter csvFileWriter,
        ICsvFileGenerator csvFileGenerator,
        IEfinIndexCosmosClient indexClient,
        IReclaimConverter reclaimConverter,
        IEnvironmentValidator environmentValidator)
        : base(
            efinCosmosDbClient,
            csvFileWriter)
    {
        _csvFileGenerator = csvFileGenerator;
        _indexClient = indexClient;
        _reclaimConverter = reclaimConverter;
        _environmentValidator = environmentValidator;
    }

    [Function("ProcessCreateReclaim")]
    public async Task Run(
        [TimerTrigger("%Reclaims:Create:CronExpression%")] TimerInfo myTimer,
        CancellationToken cancellationToken)
    {
        var environments = _environmentValidator.GetAllowedEnvironments();

        foreach (var environment in environments)
        {
            await Process(CosmosDbItemType.Reclaim, environment, cancellationToken);
        }
    }

    protected override async Task<ReclaimItemSet> Convert(IEnumerable<EfinItem> items)
    {
        var reclaims = items.Select(x => ((JObject)x.Value).ToObject<ReclaimItem>()).WhereNotNull();

        if (reclaims.IsNullOrEmpty())
        {
            throw new ArgumentException(nameof(reclaims));
        }

        EfinIndexItem indexItem;
        try
        {
            indexItem = await _indexClient.GetNextIndex(IndexConfiguration.Reclaim.BatchIndex, CosmosDbItemType.Reclaim);
        }
        catch (MissingConfigurationException)
        {
            await _indexClient.CreateItem(
                IndexConfiguration.Reclaim.BatchIndex,
                CosmosDbItemType.Reclaim,
                IndexConfiguration.Reclaim.BatchIndexPrefix,
                IndexConfiguration.Reclaim.BatchIndexLength);

            indexItem = await _indexClient.GetNextIndex(IndexConfiguration.Reclaim.BatchIndex, CosmosDbItemType.Reclaim);
        }

        var batchRef = indexItem.GetCurrentId();
        var batchNumber = indexItem.GetCurrentIndex();

        var itemSet = new ReclaimItemSet
        {
            CLI_IW_BAT = await _reclaimConverter.CreateBatch(reclaims, batchRef),
            BatchNumber = batchNumber,
            IdempotencyKey = items.First().IdempotencyKey,
        };

        foreach (var reclaimItem in reclaims)
        {
            reclaimItem.SetBatchRef(batchRef);
            itemSet.CLI_IW_ILTes.Add(reclaimItem.CliIwIlt);
            itemSet.CLI_IW_INAes.Add(reclaimItem.CliIwIna);
            itemSet.CLI_IW_INLes.Add(reclaimItem.CliIwInl);
            itemSet.CLI_IW_INVes.Add(reclaimItem.CliIwInv);
            itemSet.CLI_IW_ITLes.Add(reclaimItem.CliIwItl);
        }

        return itemSet;
    }

    protected override IEnumerable<BlobData> PrepareFiles(ReclaimItemSet convertedData)
    {
        return
        [
            _csvFileGenerator.GenerateFile(
                convertedData.CLI_IW_BAT.AsEnumerable(),
                EfinConstants.FileNamePrefix.Reclaim.CliIwBat,
                convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(
                convertedData.CLI_IW_ILTes,
                EfinConstants.FileNamePrefix.Reclaim.CliIwIlt,
                convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(
                convertedData.CLI_IW_INAes,
                EfinConstants.FileNamePrefix.Reclaim.CliIwIna,
                convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(
                convertedData.CLI_IW_INLes,
                EfinConstants.FileNamePrefix.Reclaim.CliIwInl,
                convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(
                convertedData.CLI_IW_INVes,
                EfinConstants.FileNamePrefix.Reclaim.CliIwInv,
                convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(
                convertedData.CLI_IW_ITLes,
                EfinConstants.FileNamePrefix.Reclaim.CliIwItl,
                convertedData.BatchNumber),
        ];
    }
}
