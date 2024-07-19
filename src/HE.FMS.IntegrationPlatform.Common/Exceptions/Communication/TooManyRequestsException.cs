using System.Net;

namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public class TooManyRequestsException : CommunicationException
{
    public TooManyRequestsException()
        : base(HttpStatusCode.TooManyRequests, "Service is busy, to many requests.")
    {
    }
}
