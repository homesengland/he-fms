using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Providers.Efin;

public interface ICsvFileGenerator
{
    BlobData GenerateFile(IEnumerable<object> items, string fileName, string batchNumber);
}
