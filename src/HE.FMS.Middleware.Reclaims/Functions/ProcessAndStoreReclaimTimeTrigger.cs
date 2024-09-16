using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Providers.Efin;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json.Linq;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Reclaims.Functions;

public class ProcessAndStoreReclaimTimeTrigger : DataExportFunctionBase<ReclaimItemSet>
{
    private readonly ICsvFileGenerator _csvFileGenerator;
    private readonly IEfinCosmosConfigClient _configurationClient;

    public ProcessAndStoreReclaimTimeTrigger(
        IEfinCosmosClient efinCosmosDbClient,
        ICsvFileWriter csvFileWriter,
        ICsvFileGenerator csvFileGenerator,
        IEfinCosmosConfigClient configurationClient)
        : base(
            efinCosmosDbClient,
            csvFileWriter)
    {
        _csvFileGenerator = csvFileGenerator;
        _configurationClient = configurationClient;
    }

    [Function("ProcessCreateReclaim")]
    public async Task Run(
        [TimerTrigger("%Reclaims:Create:CronExpression%")] TimerInfo myTimer,
        CancellationToken cancellationToken)
    {
        await Process(CosmosDbItemType.Reclaim, cancellationToken);
    }

    protected override async Task<ReclaimItemSet> Convert(IEnumerable<EfinItem> items)
    {
        var reclaims = items.Select(x => ((JObject)x.Value).ToObject<ReclaimItem>()).WhereNotNull();

        if (reclaims.IsNullOrEmpty())
        {
            throw new ArgumentException(nameof(reclaims));
        }

        EfinConfigItem efinConfigItem;
        try
        {
            efinConfigItem = await _configurationClient.GetNextIndex(IndexConfiguration.Reclaim.BatchIndex, CosmosDbItemType.Reclaim);
        }
        catch (MissingConfigurationException)
        {
            await _configurationClient.CreateItem(
                IndexConfiguration.Reclaim.BatchIndex,
                CosmosDbItemType.Reclaim,
                IndexConfiguration.Reclaim.BatchIndexPrefix,
                IndexConfiguration.Reclaim.BatchIndexLength);

            efinConfigItem = await _configurationClient.GetNextIndex(IndexConfiguration.Reclaim.BatchIndex, CosmosDbItemType.Reclaim);
        }

        var batchRef = efinConfigItem.ToString();
        var batchNumber = efinConfigItem.IndexNumberToString();

        var itemSet = new ReclaimItemSet
        {
            CLI_IW_BAT = CLI_IW_BAT.Create(reclaims, batchRef),
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
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_BAT.AsEnumerable(), CLI_IW_BAT.FileName, convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_ILTes, CLI_IW_ILT.FileName, convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_INAes, CLI_IW_INA.FileName, convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_INLes, CLI_IW_INL.FileName, convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_INVes, CLI_IW_INV.FileName, convertedData.BatchNumber),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_ITLes, CLI_IW_ITL.FileName, convertedData.BatchNumber),
        ];
    }
}
