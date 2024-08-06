namespace HE.FMS.Middleware.Common.Exceptions.Communication;

public static class CommunicationErrorCodes
{
    public static string ExternalSystemError => nameof(ExternalSystemError);

    public static string UnexpectedExternalSystemError => nameof(UnexpectedExternalSystemError);

    public static string InvalidExternalSystemRequest => nameof(InvalidExternalSystemRequest);

    public static string ExternalSystemNoAuthorized => nameof(ExternalSystemNoAuthorized);

    public static string ExternalResourceNotFound => nameof(ExternalResourceNotFound);

    public static string ExternalRequestProcessing => nameof(ExternalRequestProcessing);

    public static string ExternalSystemIsBusy => nameof(ExternalSystemIsBusy);

    public static string ExternalSystemInvalidResponseError => nameof(ExternalSystemInvalidResponseError);
}
