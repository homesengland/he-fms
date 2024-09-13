using System.Globalization;
using System.Text;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.Providers.Efin;
public class EfinCsvFileGenerator : ICsvFileGenerator
{
    private const char Separator = ',';
    private const string FileExtension = ".csv";
    private const string DateTimeFormat = "yyyyMMdd_HHmmss";

    private readonly IDateTimeProvider _dateTimeProvider;

    public EfinCsvFileGenerator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

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
            Name = $"{type.Name}_{_dateTimeProvider.UtcNow.ToString(DateTimeFormat, CultureInfo.InvariantCulture)}{FileExtension}",
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
