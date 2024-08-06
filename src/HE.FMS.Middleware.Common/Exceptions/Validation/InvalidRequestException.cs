namespace HE.FMS.Middleware.Common.Exceptions.Validation;

public sealed class InvalidRequestException : ValidationException
{
    public InvalidRequestException(params string[] validationMessages)
        : base(ValidationErrorCodes.InvalidRequest, "Request body is invalid. " + string.Join(' ', validationMessages))
    {
    }
}
