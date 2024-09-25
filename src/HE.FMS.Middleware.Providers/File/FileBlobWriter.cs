using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Providers.File;

public class FileBlobWriter : IFileWriter
{
    private readonly BlobServiceClient _blobServiceClient;

    public FileBlobWriter(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task WriteAsync(string blobContainerName, BlobData blobData)
    {
        ArgumentNullException.ThrowIfNull(blobContainerName);
        ArgumentNullException.ThrowIfNull(blobData);
        ArgumentNullException.ThrowIfNull(blobData.Name);
        ArgumentNullException.ThrowIfNull(blobData.Content);

        // Write the content to a memory stream  
        using var memoryStream = new MemoryStream();
        await using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
        await writer.WriteAsync(blobData.Content);
        await writer.FlushAsync();
        memoryStream.Position = 0;

        var arrayPath = blobContainerName.Split('/');
        var buildPath = new StringBuilder();
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

        for (var i = 0; i < arrayPath.Length; i++)
        {
            buildPath.Append(arrayPath[i]);
            containerClient = _blobServiceClient.GetBlobContainerClient(buildPath.ToString());
            await containerClient.CreateIfNotExistsAsync();
            buildPath.Append('/');
        }

        var blobClient = containerClient.GetBlobClient(blobData.Name);
        await blobClient.UploadAsync(memoryStream, overwrite: true);
    }
}
