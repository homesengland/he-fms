namespace HE.FMS.Middleware.Common.Exceptions.Validation;

public sealed class MissingRequestException : ValidationException
{
    public MissingRequestException()
        : base(ValidationErrorCodes.MissingRequest, "Request body is missing.")
    {
    }
}
