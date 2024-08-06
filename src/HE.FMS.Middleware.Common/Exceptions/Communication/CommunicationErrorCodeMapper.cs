using System.Net;

namespace HE.FMS.Middleware.Common.Exceptions.Communication;

internal static class CommunicationErrorCodeMapper
{
    public static string Map(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.Processing => CommunicationErrorCodes.ExternalRequestProcessing,
            HttpStatusCode.BadRequest => CommunicationErrorCodes.InvalidExternalSystemRequest,
            HttpStatusCode.Unauthorized => CommunicationErrorCodes.ExternalSystemNoAuthorized,
            HttpStatusCode.Forbidden => CommunicationErrorCodes.ExternalSystemNoAuthorized,
            HttpStatusCode.NotFound => CommunicationErrorCodes.ExternalResourceNotFound,
            HttpStatusCode.RequestTimeout => CommunicationErrorCodes.ExternalSystemIsBusy,
            HttpStatusCode.TooManyRequests => CommunicationErrorCodes.ExternalSystemIsBusy,
            HttpStatusCode.InternalServerError => CommunicationErrorCodes.ExternalSystemError,
            _ => CommunicationErrorCodes.UnexpectedExternalSystemError,
        };
    }
}
