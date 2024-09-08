namespace HE.FMS.Middleware.Providers.CsvFile;

public interface ICsvFileWriter
{
    Task WriteToCsvAsync<T>(IEnumerable<T> data, string blobContainerName, string blobName);
}
