using System.Net;

namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public class TimeoutException : CommunicationException
{
    public TimeoutException()
        : base(HttpStatusCode.RequestTimeout, "External service takes too long to respond.")
    {
    }
}
