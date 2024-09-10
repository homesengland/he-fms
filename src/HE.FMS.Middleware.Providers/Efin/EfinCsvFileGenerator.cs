using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Providers.Efin;
public class EfinCsvFileGenerator : ICsvFileGenerator
{
    private const char Separator = ',';

    public BlobData GenerateFile(IEnumerable<object> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        var type = items.GetType().GetGenericArguments()[0];

        var rows = new List<string>
        {
            GenerateHeader(type),
        };

        foreach (var item in items)
        {
            rows.Add(GenerateRow(item));
        }

        return new BlobData()
        {
            Name = $"{type.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
            Content = string.Join(Environment.NewLine, rows),
        };
    }

    private string GenerateRow(object item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var type = item.GetType();

        var rowSize = type.GetClassAttributeValue((EfinFileRowSizeAttribute rowSizeAttribute) => rowSizeAttribute.RowSize);

        var row = new string(Separator, rowSize);

        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var (startIndex, endIndex) = property.GetPropertyAttributeValue((EfinFileRowIndexAttribute rowIndexAttribute) => (rowIndexAttribute.StartIndex, rowIndexAttribute.EndIndex));
            var length = endIndex - startIndex;// + 1;
            var value = property.GetValue(item, null)?.ToString() ?? string.Empty;

            row = row.ReplaceAt(startIndex, length, value, ' ');
        }

        return row;
    }

    private string GenerateHeader(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var properties = type.GetProperties();

        var names = properties.OrderBy(property => property.GetPropertyAttributeValue((EfinFileRowIndexAttribute rowIndexAttribute) => rowIndexAttribute.StartIndex)).Select(property => property.Name).ToArray();

        return string.Join(Separator, names);
    }
}
