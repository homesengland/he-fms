using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.FMS.Middleware.Common.Exceptions.Validation;
public sealed class MissingRequiredHeaderException : ValidationException
{
    public MissingRequiredHeaderException(string headerName)
        : base(ValidationErrorCodes.MissingRequiredHeader, $"Required header {headerName} is missing.")
    {
    }
}
