namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Settings;

internal sealed class MambuApiSettings : IMambuApiSettings
{
    public Uri BaseUrl { get; set; }

    public int RetryCount { get; set; }

    public int RetryDelayInMilliseconds { get; set; }
}
