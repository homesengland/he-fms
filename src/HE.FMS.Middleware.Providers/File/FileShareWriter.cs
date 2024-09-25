using System.Text;
using Azure.Storage.Files.Shares;
using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Providers.File;

public class FileShareWriter : IFileWriter
{
    private readonly ShareClient _shareClient;

    public FileShareWriter(ShareClient shareClient)
    {
        _shareClient = shareClient;
    }

    public async Task WriteAsync(string blobContainerName, BlobData blobData)
    {
        ArgumentNullException.ThrowIfNull(blobContainerName);
        ArgumentNullException.ThrowIfNull(blobData);
        ArgumentNullException.ThrowIfNull(blobData.Name);
        ArgumentNullException.ThrowIfNull(blobData.Content);

        await _shareClient.CreateIfNotExistsAsync();

        var arrayPath = blobContainerName.Split('/');
        var buildPath = new StringBuilder();
        var directoryClient = _shareClient.GetRootDirectoryClient();

        for (var i = 0; i < arrayPath.Length; i++)
        {
            buildPath.Append(arrayPath[i]);
            directoryClient = _shareClient.GetDirectoryClient(buildPath.ToString());
            await directoryClient.CreateIfNotExistsAsync();
            buildPath.Append('/');
        }

        var fileClient = directoryClient.GetFileClient(blobData.Name);
        var fileContent = Encoding.UTF8.GetBytes(blobData.Content);

        using var stream = new MemoryStream(fileContent);
        await fileClient.CreateAsync(stream.Length);
        await fileClient.UploadRangeAsync(new Azure.HttpRange(0, stream.Length), stream);
    }
}
