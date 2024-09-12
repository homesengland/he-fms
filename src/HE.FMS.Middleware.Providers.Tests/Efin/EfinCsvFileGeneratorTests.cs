using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Providers.Efin;
using HE.FMS.Middleware.Providers.Tests.Fakes;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.Efin;
public class EfinCsvFileGeneratorTests
{
    private readonly EfinCsvFileGenerator _csvFileGenerator;

    public EfinCsvFileGeneratorTests()
    {
        _csvFileGenerator = new EfinCsvFileGenerator(new FakeDateTimeProvider());
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
        var result = _csvFileGenerator.GenerateFile(items);

        // Assert
        Assert.Equal("TestItem_" + DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".csv", result.Name);
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
