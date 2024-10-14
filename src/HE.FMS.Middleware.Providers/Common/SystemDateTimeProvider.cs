namespace HE.FMS.Middleware.Providers.Common;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime Today => DateTime.Today;

    public DateTime UtcNow => DateTime.UtcNow;
}
