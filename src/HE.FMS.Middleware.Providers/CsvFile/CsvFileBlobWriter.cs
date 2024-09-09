using System.Globalization;
using System.Reflection;
using System.Text;
using Azure.Storage.Blobs;

namespace HE.FMS.Middleware.Providers.CsvFile;

public class CsvFileBlobWriter : ICsvFileWriter
{
    private static readonly System.Buffers.SearchValues<char> EscapeChars = System.Buffers.SearchValues.Create(",\n\r\"");
    private readonly BlobServiceClient _blobServiceClient;

    public CsvFileBlobWriter(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task WriteToCsvAsync<T>(IEnumerable<T> data, string blobContainerName, string blobName)
    {
        ArgumentNullException.ThrowIfNull(data);

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Write the CSV content to a memory stream  
        using var memoryStream = new MemoryStream();
        await using var writer = new StreamWriter(memoryStream, Encoding.UTF8);

        // Write the header line  
        await writer.WriteLineAsync(string.Join(",", properties.Select(p => p.Name)));

        // Write the data lines  
        foreach (var item in data)
        {
            await writer.WriteLineAsync(string.Join(",", properties.Select(p => ConvertToCsvValue(p.GetValue(item)))));
        }

        await writer.FlushAsync();
        memoryStream.Position = 0;

        // Upload the memory stream to Blob Storage  
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
        await containerClient.CreateIfNotExistsAsync();
        var blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.UploadAsync(memoryStream, overwrite: true);
    }

    private static string ConvertToCsvValue(object? value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        if (value is string str)
        {
            return EscapeCsvValue(str);
        }

        return value is IFormattable formattable ? formattable.ToString(null, CultureInfo.InvariantCulture) : EscapeCsvValue(value.ToString());
    }

    private static string EscapeCsvValue(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        // Escape double quotes by replacing each one with two double quotes  
        value = value.Replace("\"", "\"\"");

        // If the value contains a comma, a newline, or a double quote, then wrap it in double quotes  
        return value.AsSpan().IndexOfAny(EscapeChars) != -1 ? $"\"{value}\"" : value;
    }
}
