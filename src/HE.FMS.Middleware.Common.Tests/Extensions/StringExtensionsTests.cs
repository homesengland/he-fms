using HE.FMS.Middleware.Common.Extensions;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Extensions;
public class StringExtensionsTests
{
    [Fact]
    public void Should_remove_spaces_and_dashes()
    {
        const string input = "Arms Length Body of Government";
        const string expectedOutput = "ArmsLengthBodyofGovernment";

        var result = input.RemoveSpecialCharacters();

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void Should_remove_spaces_and_slashes()
    {
        const string input = "Public/Private Partnership";
        const string expectedOutput = "PublicPrivatePartnership";

        var result = input.RemoveSpecialCharacters();

        Assert.Equal(expectedOutput, result);
    }
}
