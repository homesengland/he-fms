using HE.FMS.Middleware.Providers.Common;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.Common;

public class SystemDateTimeProviderTests
{
    private readonly IDateTimeProvider _dateTimeProvider = new SystemDateTimeProvider();

    [Fact]
    public void Now_ShouldReturnCurrentDateTime()
    {
        // Arrange  
        var before = DateTime.Now;

        // Act  
        var now = _dateTimeProvider.Now;

        // Assert  
        var after = DateTime.Now;
        Assert.True(now >= before && now <= after);
    }

    [Fact]
    public void Today_ShouldReturnCurrentDate()
    {
        // Arrange  
        var before = DateTime.Today;

        // Act  
        var today = _dateTimeProvider.Today;

        // Assert  
        var after = DateTime.Today;
        Assert.Equal(before, today);
        Assert.Equal(after, today);
    }

    [Fact]
    public void UtcNow_ShouldReturnCurrentUtcDateTime()
    {
        // Arrange  
        var before = DateTime.UtcNow;

        // Act  
        var utcNow = _dateTimeProvider.UtcNow;

        // Assert  
        var after = DateTime.UtcNow;
        Assert.True(utcNow >= before && utcNow <= after);
    }
}
