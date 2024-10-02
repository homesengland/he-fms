using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.Common.Settings;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.Common;

public class EnvironmentValidatorTests
{
    [Fact]
    public void Validate_ShouldThrowInvalidEnvironmentException_WhenEnvironmentIsNull()
    {
        // Arrange  
        var settings = new AllowedEnvironmentSettings("Development,Production");
        var validator = new EnvironmentValidator(settings);

        // Act & Assert  
        Assert.Throws<InvalidEnvironmentException>(() => validator.Validate(null));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidEnvironmentException_WhenEnvironmentIsWhitespace()
    {
        // Arrange  
        var settings = new AllowedEnvironmentSettings("Development,Production");
        var validator = new EnvironmentValidator(settings);

        // Act & Assert  
        Assert.Throws<InvalidEnvironmentException>(() => validator.Validate("    "));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidEnvironmentException_WhenEnvironmentIsNotAllowed()
    {
        // Arrange  
        var settings = new AllowedEnvironmentSettings("Development,Production");
        var validator = new EnvironmentValidator(settings);

        // Act & Assert  
        Assert.Throws<InvalidEnvironmentException>(() => validator.Validate("Staging"));
    }

    [Fact]
    public void Validate_ShouldNotThrow_WhenEnvironmentIsAllowed()
    {
        // Arrange  
        var settings = new AllowedEnvironmentSettings("Development,Production");

        var validator = new EnvironmentValidator(settings);

        // Act & Assert  
        validator.Validate("Development");
    }

    [Fact]
    public void GetAllowedEnvironments_ShouldReturnCorrectEnvironments()
    {
        // Arrange  
        var expectedEnvironments = new[] { "Development", "Production" };
        var settings = new AllowedEnvironmentSettings("Development,Production");
        var validator = new EnvironmentValidator(settings);

        // Act  
        var allowedEnvironments = validator.GetAllowedEnvironments();

        // Assert  
        Assert.Equal(expectedEnvironments, allowedEnvironments);
    }
}
