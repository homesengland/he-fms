using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Providers.File;

public interface IFileWriter
{
    Task WriteAsync(string blobContainerName, BlobData blobData);
}
