using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.BusinessLogic.Efin;

public interface ICsvFileGenerator
{
    BlobData GenerateFile(IEnumerable<object> items);
}
