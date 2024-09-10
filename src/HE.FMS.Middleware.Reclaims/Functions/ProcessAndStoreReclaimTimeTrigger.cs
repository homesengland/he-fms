using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Providers.Efin;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;

namespace HE.FMS.Middleware.Reclaims.Functions;

public class ProcessAndStoreReclaimTimeTrigger : DataExportFunctionBase<ReclaimItemSet>
{
    private readonly IReclaimConverter _reclaimConverter;
    private readonly ICsvFileGenerator _csvFileGenerator;

    public ProcessAndStoreReclaimTimeTrigger(
        IEfinCosmosClient efinCosmosDbClient,
        ICsvFileWriter csvFileWriter,
        IReclaimConverter reclaimConverter,
        ICsvFileGenerator csvFileGenerator)
        : base(
            efinCosmosDbClient,
            csvFileWriter)
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

    protected override async Task<ReclaimItemSet> Convert(IEnumerable<EfinItem> items)
    {
        var reclaims = items.Where(x => x.Value is ReclaimPaymentRequest)
            .Select(x => x.Value as ReclaimPaymentRequest).WhereNotNull().ToList();

        if (reclaims.IsNullOrEmpty())
        {
            throw new ArgumentException(nameof(reclaims));
        }

        var itemSet = new ReclaimItemSet
        {
            CLI_IW_BAT = CLI_IW_BAT.Create(reclaims),
            IdempotencyKey = items.First().IdempotencyKey,
        };

        foreach (var reclaimPaymentRequest in reclaims)
        {
            var item = await _reclaimConverter.Convert(reclaimPaymentRequest);
            itemSet.CLI_IW_ILTes.Add(item.CliIwIlt);
            itemSet.CLI_IW_INAes.Add(item.CliIwIna);
            itemSet.CLI_IW_INLes.Add(item.CliIwInl);
            itemSet.CLI_IW_INVes.Add(item.CliIwInv);
            itemSet.CLI_IW_ITLes.Add(item.CliIwItl);
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
