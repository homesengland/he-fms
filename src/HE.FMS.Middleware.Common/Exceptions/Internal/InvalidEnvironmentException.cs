using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.FMS.Middleware.Common.Exceptions.Internal;

public class InvalidEnvironmentException : InternalSystemException
{
    public InvalidEnvironmentException(string environment)
        : base($"Environment {environment} is not allowed for this instance.")
    {
    }

    public override string ErrorCode => InternalErrorCodes.InvalidEnvironment;
}
