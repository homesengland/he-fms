using System.Text;
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

        var rows = new List<string>();

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
