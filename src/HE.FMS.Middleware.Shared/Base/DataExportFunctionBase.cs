using System.Globalization;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.File;

namespace HE.FMS.Middleware.Shared.Base;

public abstract class DataExportFunctionBase<T>
    where T : IItemSet
{
    private readonly IEfinCosmosClient _efinCosmosDbClient;
    private readonly IFileWriter _csvFileWriter;

    protected DataExportFunctionBase(
        IEfinCosmosClient efinCosmosDbClient,
        IFileWriter csvFileWriter)
    {
        _csvFileWriter = csvFileWriter;
        _efinCosmosDbClient = efinCosmosDbClient;
    }

    protected async Task Process(CosmosDbItemType type, CancellationToken cancellationToken)
    {
        var items = await _efinCosmosDbClient.GetAllNewItemsAsync(type);

        if (items.IsNullOrEmpty())
        {
            return;
        }

        await _efinCosmosDbClient.ChangeItemsStatusAsync(items, CosmosDbItemStatus.InProgress, cancellationToken);

        var convertedData = await Convert(items);

        var blobs = PrepareFiles(convertedData);

        if (blobs.Any())
        {
            foreach (var blob in blobs)
            {
                await _csvFileWriter.WriteAsync(
                    $"{type.ToString().ToLower(CultureInfo.InvariantCulture)}-container",
                    blob);
            }
        }

        await _efinCosmosDbClient.ChangeItemsStatusAsync(items, CosmosDbItemStatus.Processed, cancellationToken);
    }

    protected abstract Task<T> Convert(IEnumerable<EfinItem> items);

    protected abstract IEnumerable<BlobData> PrepareFiles(T convertedData);
}
