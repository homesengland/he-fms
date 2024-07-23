namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Settings;

public interface IMambuApiSettings
{
    Uri BaseUrl { get; }

    int RetryCount { get; }

    int RetryDelayInMilliseconds { get; }
}
