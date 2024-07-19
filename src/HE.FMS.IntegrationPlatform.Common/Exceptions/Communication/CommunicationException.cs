using System.Net;

namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public abstract class CommunicationException : Exception
{
    protected CommunicationException(HttpStatusCode code, string message)
        : base(message)
    {
        Code = code;
    }

    public HttpStatusCode Code { get; }
}
