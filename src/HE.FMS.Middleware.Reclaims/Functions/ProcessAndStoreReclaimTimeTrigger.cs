using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Providers.Efin;
using HE.FMS.Middleware.Providers.ServiceBus;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json.Linq;

namespace HE.FMS.Middleware.Reclaims.Functions;

public class ProcessAndStoreReclaimTimeTrigger : DataExportFunctionBase<ReclaimItemSet>
{
    private readonly ICsvFileGenerator _csvFileGenerator;

    public ProcessAndStoreReclaimTimeTrigger(
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

    [Function("ProcessCreateReclaim")]
    public async Task Run(
        [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
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

        var itemSet = new ReclaimItemSet
        {
            CLI_IW_BAT = CLI_IW_BAT.Create(reclaims),
            IdempotencyKey = items.First().IdempotencyKey,
        };

        foreach (var reclaimItem in reclaims)
        {
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

            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_BAT.AsEnumerable()),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_ILTes),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_INAes),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_INLes),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_INVes),
            _csvFileGenerator.GenerateFile(convertedData.CLI_IW_ITLes),
        ];
    }
}
