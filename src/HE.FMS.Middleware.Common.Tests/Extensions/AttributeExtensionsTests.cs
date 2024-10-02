using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Tests.Fakes;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Extensions;

public class AttributeExtensionsTests
{
    [Fact]
    public void GetClassAttributeValue_ShouldReturnAttributeValue_WhenAttributeIsPresent()
    {
        // Arrange  
        var type = typeof(FakeAttributeClass);

        // Act  
        var result = type.GetClassAttributeValue<FakeTestAttribute, string>(att => att.Value);

        // Assert  
        Assert.Equal("ClassAttributeValue", result);
    }

    [Fact]
    public void GetClassAttributeValue_ShouldReturnNull_WhenAttributeIsNotPresent()
    {
        // Arrange  
        var type = typeof(FakeNoAttributeClass);

        // Act  
        var result = type.GetClassAttributeValue<FakeTestAttribute, string>(att => att.Value);

        // Assert  
        Assert.Null(result);
    }

    [Fact]
    public void GetPropertyAttributeValue_ShouldReturnAttributeValue_WhenAttributeIsPresent()
    {
        // Arrange  
        var property = typeof(FakeAttributeClass).GetProperty(nameof(FakeAttributeClass.TestProperty));

        // Act  
        var result = property!.GetPropertyAttributeValue<FakeTestAttribute, string>(att => att.Value);

        // Assert  
        Assert.Equal("PropertyAttributeValue", result);
    }

    [Fact]
    public void GetPropertyAttributeValue_ShouldReturnNull_WhenAttributeIsNotPresent()
    {
        // Arrange  
        var property = typeof(FakeNoAttributeClass).GetProperty(nameof(FakeNoAttributeClass.AnotherProperty));

        // Act  
        var result = property!.GetPropertyAttributeValue<FakeTestAttribute, string>(att => att.Value);

        // Assert  
        Assert.Null(result);
    }
}
