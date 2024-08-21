namespace HE.FMS.Middleware.Common.Exceptions.Internal;
public abstract class InternalSystemException : Exception
{
    protected InternalSystemException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }

    public abstract string ErrorCode { get; }
}
