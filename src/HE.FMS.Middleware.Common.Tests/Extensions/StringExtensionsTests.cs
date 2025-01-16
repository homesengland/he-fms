using HE.FMS.Middleware.Common.Extensions;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Extensions;
public class StringExtensionsTests
{
    [Fact]
    public void Should_remove_commas()
    {
        const string input = "Arms, Length Body ,of, Government";
        const string expectedOutput = "Arms Length Body of Government";

        var result = input.RemoveSpecialCharacters();

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void Should_remove_slashes()
    {
        const string input = "Public/Private Partnership";
        const string expectedOutput = "PublicPrivate Partnership";

        var result = input.RemoveSpecialCharacters();

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void Should_skip_underscores()
    {
        const string input = "Public_Private_Partnership";
        const string expectedOutput = "Public_Private_Partnership";

        var result = input.RemoveSpecialCharacters();

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void Should_skip_dashes()
    {
        const string input = "Public-Private-Partnership";
        const string expectedOutput = "Public-Private-Partnership";

        var result = input.RemoveSpecialCharacters();

        Assert.Equal(expectedOutput, result);
    }
}
