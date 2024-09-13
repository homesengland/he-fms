namespace HE.FMS.Middleware.Providers.Common;

public interface IDateTimeProvider
{
    DateTime Now { get; }

    DateTime Today { get; }

    DateTime UtcNow { get; }
}
