using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
public class FakeDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => new(2000, 1, 1, 1, 1, 1, DateTimeKind.Utc);

    public DateTime Today => new(2000, 1, 1, 1, 1, 1, DateTimeKind.Utc);

    public DateTime UtcNow => new(2000, 1, 1, 1, 1, 1, DateTimeKind.Utc);
}
