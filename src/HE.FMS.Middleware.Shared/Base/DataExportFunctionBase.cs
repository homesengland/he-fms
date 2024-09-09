using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CsvFile;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.ServiceBus;

namespace HE.FMS.Middleware.Shared.Base;

public abstract class DataExportFunctionBase<T>
    where T : IItemSet
{
    private readonly IDbItemClient _dbItemClient;
    private readonly ICsvFileWriter _csvFileWriter;
    private readonly TopicClient _topicClient;
    private readonly IObjectSerializer _objectSerializer;

    protected DataExportFunctionBase(
        IDbItemClient dbItemClient,
        ICsvFileWriter csvFileWriter,
        TopicClient topicClient,
        IObjectSerializer objectSerializer)
    {
        _csvFileWriter = csvFileWriter;
        _dbItemClient = dbItemClient;
        _topicClient = topicClient;
        _objectSerializer = objectSerializer;
    }

    protected async Task Process(CosmosDbItemType type, CancellationToken cancellationToken)
    {
        var items = await _dbItemClient.GetAllNewItemsAsync(type);

        var convertedData = Convert(items);

        var blobs = PrepareFiles(convertedData);

        if (blobs.Any())
        {
            foreach (var blob in blobs)
            {
                await _csvFileWriter.WriteToBlobAsync(
                    $"{type.ToString().ToLower(CultureInfo.InvariantCulture)}-container",
                    blob);
            }

            await _dbItemClient.UpdateItemStatusAsync(items, CosmosDbItemStatus.Completed, cancellationToken);
        }

        var topicOutput = new Message(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(convertedData!)))
        {
            CorrelationId = convertedData.IdempotencyKey,
        };

        await _topicClient.SendAsync(topicOutput);
    }

    protected abstract T Convert(IEnumerable<CosmosDbItem> items);

    protected abstract IEnumerable<BlobData> PrepareFiles(T convertedData);
}
