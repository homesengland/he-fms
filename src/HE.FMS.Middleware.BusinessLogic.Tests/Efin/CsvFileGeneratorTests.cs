using System.Globalization;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Providers.Common;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;
public class CsvFileGeneratorTests
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly CsvFileGenerator _csvFileGenerator;

    public CsvFileGeneratorTests()
    {
        _dateTimeProvider = new FakeDateTimeProvider();
        _csvFileGenerator = new CsvFileGenerator(_dateTimeProvider);
    }

    [Fact]
    public void GenerateFile_ShouldReturnExpectedBlobData()
    {
        // Arrange
        var items = new List<TestItem>
        {
            new() { Id = 1, Name = "Item1" },
            new() { Id = 2, Name = "Item2" },
        };

        var expectedContent = "1,Item1" + Environment.NewLine + "2,Item2";

        // Act
        var result = _csvFileGenerator.GenerateFile(items, "FileName", "000001");

        // Assert
        Assert.Equal("FileName" + "000001_" + _dateTimeProvider.UtcNow.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".csv", result.Name);
        Assert.Equal(expectedContent, result.Content);
    }

    private sealed class TestItem
    {
        [EfinFileRowIndex(1, 1)]
        public int Id { get; set; }

        [EfinFileRowIndex(2, 10)]
        public string Name { get; set; }
    }
}
