using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CsvFile;

namespace HE.FMS.Middleware.Shared.Base;

public class DataExportFunctionBase
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

        if (items.Count > 0)
        {
            await _csvFileWriter.WriteToCsvAsync(
                items,
                $"{type.ToString().ToLower(CultureInfo.InvariantCulture)}-container",
                $"{type}-{DateTime.Now:yyyyMMdd_HHmmss}.csv");

            await _dbItemClient.UpdateItemStatusAsync(items, CosmosDbItemStatus.Completed, cancellationToken);
        }
    }
}
