using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CsvFile;

namespace HE.FMS.Middleware.Shared.Base;

public abstract class DataExportFunctionBase
{
    private readonly IDbItemClient _dbItemClient;
    private readonly ICsvFileWriter _csvFileWriter;

    protected DataExportFunctionBase(IDbItemClient dbItemClient, ICsvFileWriter csvFileWriter)
    {
        _csvFileWriter = csvFileWriter;
        _dbItemClient = dbItemClient;
    }

    protected async Task Process(CosmosDbItemType type, CancellationToken cancellationToken)
    {
        var items = await _dbItemClient.GetAllNewItemsAsync(type);

        var blobs = Convert(items);

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
    }

    protected abstract IEnumerable<BlobData> Convert(IEnumerable<CosmosDbItem> items);
}
