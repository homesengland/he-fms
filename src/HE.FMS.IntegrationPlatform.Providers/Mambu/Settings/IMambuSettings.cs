namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Settings;

public interface IMambuSettings
{
    Uri BaseUrl { get; }

    int RetryCount { get; }

    int RetryDelayInMilliseconds { get; }
}
