using System.Globalization;
using System.Reflection;
using System.Text;
using Azure.Storage.Blobs;
using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Providers.CsvFile;

public class CsvFileBlobWriter : ICsvFileWriter
{
    private readonly BlobServiceClient _blobServiceClient;

    public CsvFileBlobWriter(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task WriteToBlobAsync(string blobContainerName, BlobData blobData)
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

        // Upload the memory stream to Blob Storage  
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
        await containerClient.CreateIfNotExistsAsync();
        var blobClient = containerClient.GetBlobClient(blobData.Name);
        await blobClient.UploadAsync(memoryStream, overwrite: true);
    }
}
