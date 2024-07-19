using System.Net;

namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public class ExternalServerErrorException : CommunicationException
{
    public ExternalServerErrorException()
        : base(HttpStatusCode.InternalServerError, "External server error.")
    {
    }

    public ExternalServerErrorException(Exception ex)
        : base(HttpStatusCode.InternalServerError, ex.Message)
    {
    }
}
