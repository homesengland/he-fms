using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Providers.CsvFile;

public interface ICsvFileWriter
{
    Task WriteAsync(string blobContainerName, BlobData blobData);
}
