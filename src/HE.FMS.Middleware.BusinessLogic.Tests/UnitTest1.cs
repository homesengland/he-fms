using FluentAssertions;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests;

public class UnitTest1
{
    [Fact]
    public void FirstTest()
    {
        var x = string.Empty;
        x.Should().Be(string.Empty);
    }
}
