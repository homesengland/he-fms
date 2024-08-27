using FluentAssertions;
using Xunit;

namespace HE.FMS.Middleware.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var x = string.Empty;
        x.Should().Be(string.Empty);
    }
}
