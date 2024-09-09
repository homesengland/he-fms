using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Providers.CsvFile;

public interface ICsvFileWriter
{
    Task WriteToBlobAsync(string blobContainerName, BlobData blobData);
}
