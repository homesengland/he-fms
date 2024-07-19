using System.Net;

namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public sealed class ExternalSystemCommunicationException : ExternalSystemException
{
    public ExternalSystemCommunicationException(string systemName, HttpStatusCode code, Exception? innerException = null)
        : base($"Error while accessing {systemName} external system", innerException)
    {
        Code = code;
    }

    public HttpStatusCode Code { get; }

    public override string ErrorCode => CommunicationErrorCodeMapper.Map(Code);
}
