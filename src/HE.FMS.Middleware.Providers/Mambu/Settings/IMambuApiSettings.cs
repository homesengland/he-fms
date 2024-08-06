namespace HE.FMS.Middleware.Providers.Mambu.Settings;

internal interface IMambuApiSettings
{
    Uri BaseUrl { get; }

    int RetryCount { get; }

    int RetryDelayInMilliseconds { get; }
}
