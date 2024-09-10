using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using HE.FMS.Middleware.Providers.CsvFile;

namespace HE.FMS.Middleware.Shared.Base;

public abstract class DataExportFunctionBase<T>
    where T : IItemSet
{
    private readonly IEfinCosmosClient _efinCosmosDbClient;
    private readonly ICsvFileWriter _csvFileWriter;

    protected DataExportFunctionBase(
        IEfinCosmosClient efinCosmosDbClient,
        ICsvFileWriter csvFileWriter)
    {
        _csvFileWriter = csvFileWriter;
        _efinCosmosDbClient = efinCosmosDbClient;
    }

    protected async Task Process(CosmosDbItemType type, CancellationToken cancellationToken)
    {
        var items = await _efinCosmosDbClient.GetAllNewItemsAsync(type);

        if (!items.Any())
        {
            return;
        }

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

            await _efinCosmosDbClient.ChangeItemsStatusAsync(items, CosmosDbItemStatus.Completed, cancellationToken);
        }
    }

    protected abstract T Convert(IEnumerable<EfinItem> items);

    protected abstract IEnumerable<BlobData> PrepareFiles(T convertedData);
}
