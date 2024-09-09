using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Providers.Efin;
using HE.FMS.Middleware.Providers.ServiceBus;
using HE.FMS.Middleware.Shared.Base;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.ServiceBus;

namespace HE.FMS.Middleware.Reclaims.Functions;

public class ProcessCreateReclaim : DataExportFunctionBase<ReclaimItemSet>
{
    private readonly IReclaimConverter _reclaimConverter;
    private readonly ICsvFileGenerator _csvFileGenerator;

    public ProcessCreateReclaim(
        IDbItemClient dbItemClient,
        ICsvFileWriter csvFileWriter,
        IReclaimConverter reclaimConverter,
        ICsvFileGenerator csvFileGenerator,
        ITopicClientFactory topicClientFactory,
        IObjectSerializer objectSerializer)
        : base(
            dbItemClient,
            csvFileWriter,
            topicClientFactory.GetTopicClient("Reclaims:Create:TopicName"),
            objectSerializer)
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

    protected override ReclaimItemSet Convert(IEnumerable<CosmosDbItem> items)
    {
        var reclaims = items.Where(x => x.Value is ReclaimPaymentRequest)
            .Select(x => x.Value as ReclaimPaymentRequest).WhereNotNull();

        if (reclaims.IsNullOrEmpty())
        {
            throw new ArgumentException(nameof(reclaims));
        }

        var itemSet = _reclaimConverter.Convert(reclaims);
        itemSet.IdempotencyKey = items.First().IdempotencyKey;

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
