using System.Net;

namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;

public class NotFoundException : CommunicationException
{
    public NotFoundException()
        : base(HttpStatusCode.NotFound, "Resource or endpoint not found.")
    {
    }
}
