namespace HE.FMS.Middleware.Providers.CsvFile.Settings;

public class IntegrationStorageSettings : IIntegrationStorageSettings
{
    public string ConnectionString { get; set; }

    public string ShareName { get; set; }
}
