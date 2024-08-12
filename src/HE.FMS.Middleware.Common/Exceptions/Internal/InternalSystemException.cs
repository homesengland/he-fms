using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.FMS.Middleware.Common.Exceptions.Common;
public abstract class InternalSystemException : Exception
{
    protected InternalSystemException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }

    public abstract string ErrorCode { get; }
}
