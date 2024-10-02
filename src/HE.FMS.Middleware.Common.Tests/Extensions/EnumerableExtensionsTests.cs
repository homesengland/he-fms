using HE.FMS.Middleware.Common.Extensions;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Extensions;

public class EnumerableExtensionsTests
{
    [Fact]
    public void IsNullOrEmpty_ShouldReturnTrue_WhenCollectionIsNull()
    {
        IEnumerable<int> collection = null!;
        var result = collection.IsNullOrEmpty();

        Assert.True(result);
    }

    [Fact]
    public void IsNullOrEmpty_ShouldReturnTrue_WhenCollectionIsEmpty()
    {
        var collection = Enumerable.Empty<int>();
        var result = collection.IsNullOrEmpty();
        Assert.True(result);
    }

    [Fact]
    public void IsNullOrEmpty_ShouldReturnFalse_WhenCollectionIsNotEmpty()
    {
        var collection = new[] { 1 };
        var result = collection.IsNullOrEmpty();
        Assert.False(result);
    }

    [Fact]
    public void DefaultIfNull_ShouldReturnEmpty_WhenCollectionIsNull()
    {
        IEnumerable<int> collection = null!;
        var result = collection.DefaultIfNull();
        Assert.Empty(result);
    }

    [Fact]
    public void DefaultIfNull_ShouldReturnEmpty_WhenCollectionIsEmpty()
    {
        var collection = Enumerable.Empty<int>();
        var result = collection.DefaultIfNull();
        Assert.Empty(result);
    }

    [Fact]
    public void DefaultIfNull_ShouldReturnOriginalCollection_WhenCollectionIsNotEmpty()
    {
        var collection = new[] { 1 };
        var result = collection.DefaultIfNull();
        Assert.Equal(collection, result);
    }

    [Fact]
    public void WhereNotNull_ShouldReturnNonNullItems_WhenCollectionHasReferenceTypes()
    {
        var collection = new string[] { "a", null!, "b", null!, "c" };
        var result = collection.WhereNotNull().ToArray();
        Assert.Equal(["a", "b", "c"], result);
    }

    [Fact]
    public void AsEnumerable_ShouldReturnEnumerableContainingSingleItem()
    {
        var item = 1;
        var result = item.AsEnumerable().ToArray();
        Assert.Single(result);
        Assert.Equal(item, result[0]);
    }
}
