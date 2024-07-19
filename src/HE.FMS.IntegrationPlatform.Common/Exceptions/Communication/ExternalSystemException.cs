namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public abstract class ExternalSystemException : Exception
{
    protected ExternalSystemException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }

    public abstract string ErrorCode { get; }
}
