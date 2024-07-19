using System.Net;

namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public class UnauthorizedException : CommunicationException
{
    public UnauthorizedException()
        : base(HttpStatusCode.Unauthorized, "Request to external service is unauthorized.")
    {
    }
}
