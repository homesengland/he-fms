using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Exceptions.Common;
using HE.FMS.Middleware.Common.Exceptions.Validation;

namespace HE.FMS.Middleware.Common.Exceptions.Internal;
public class MissingConfigurationException : InternalSystemException
{
    public MissingConfigurationException(string configName)
        : base($"Required configuration {configName} is missing.")
    {
    }

    public override string ErrorCode => InternalErrorCodes.MissingConfiguration;
}
