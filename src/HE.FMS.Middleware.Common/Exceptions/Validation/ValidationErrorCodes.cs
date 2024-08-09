namespace HE.FMS.Middleware.Common.Exceptions.Validation;

internal static class ValidationErrorCodes
{
    public static string InvalidRequest => nameof(InvalidRequest);

    public static string MissingRequest => nameof(MissingRequest);

    public static string MissingRequiredHeader => nameof(MissingRequiredHeader);

    public static string MissingRequiredField => nameof(MissingRequiredField);

    public static string FailedSerialization => nameof(FailedSerialization);

    public static string InvalidQueryParams => nameof(InvalidQueryParams);
}
