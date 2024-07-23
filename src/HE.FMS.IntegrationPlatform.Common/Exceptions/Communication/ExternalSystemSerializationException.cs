namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public sealed class ExternalSystemSerializationException : ExternalSystemException
{
    public ExternalSystemSerializationException(string systemName, string? message = null, Exception? innerException = null)
        : base(message ?? $"Error while deserializing response from {systemName} external system", innerException)
    {
    }

    public override string ErrorCode => CommunicationErrorCodes.ExternalSystemInvalidResponseError;
}
