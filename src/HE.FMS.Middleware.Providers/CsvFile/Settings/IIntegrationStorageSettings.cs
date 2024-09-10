namespace HE.FMS.Middleware.Providers.CsvFile.Settings;

public interface IIntegrationStorageSettings
{
    string ConnectionString { get; }

    string ShareName { get; }
}
