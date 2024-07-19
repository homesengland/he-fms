namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public sealed class ExternalSystemSerializationException : ExternalSystemException
{
    public ExternalSystemSerializationException(string systemName, Exception? innerException = null)
        : base($"Error while deserializing response from {systemName} external system", innerException)
    {
    }

    public override string ErrorCode => CommunicationErrorCodes.ExternalSystemInvalidResponseError;
}
