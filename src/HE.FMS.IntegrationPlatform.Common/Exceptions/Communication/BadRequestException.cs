using System.Net;

namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public class BadRequestException : CommunicationException
{
    public BadRequestException()
        : base(HttpStatusCode.BadRequest, "Provided request is invalid. Please fix request details and try again.")
    {
    }
}
