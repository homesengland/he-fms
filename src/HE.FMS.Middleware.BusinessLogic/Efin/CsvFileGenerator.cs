using System.Globalization;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.BusinessLogic.Efin;

public class CsvFileGenerator(IDateTimeProvider dateTimeProvider) : ICsvFileGenerator
{
    private const char Separator = ',';
    private const string FileExtension = ".csv";
    private const string DateTimeFormat = "yyyyMMdd";

    public BlobData GenerateFile(IEnumerable<object> items, string fileName, string batchNumber)
    {
        ArgumentNullException.ThrowIfNull(items);

        var rows = items.Select(GenerateRow).ToList();

        var dateString = dateTimeProvider.UtcNow.ToString(DateTimeFormat, CultureInfo.InvariantCulture);

        return new BlobData()
        {
            Name = $"{fileName}{batchNumber}_{dateString}{FileExtension}",
            Content = string.Join(Environment.NewLine, rows),
        };
    }

    private string GenerateRow(object item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var type = item.GetType();

        var columns = new SortedDictionary<int, string>();

        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var index = property.GetPropertyAttributeValue((EfinFileRowIndexAttribute rowIndexAttribute) => rowIndexAttribute.StartIndex);

            var value = property.GetValue(item, null)?.ToString() ?? string.Empty;

            columns.Add(index, value);
        }

        return string.Join(Separator, columns.Values.ToList());
    }
}
