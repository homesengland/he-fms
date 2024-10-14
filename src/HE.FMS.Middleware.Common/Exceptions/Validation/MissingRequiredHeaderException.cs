namespace HE.FMS.Middleware.Common.Exceptions.Validation;
public sealed class MissingRequiredHeaderException : ValidationException
{
    public MissingRequiredHeaderException(string headerName)
        : base(ValidationErrorCodes.MissingRequiredHeader, $"Required header {headerName} is missing.")
    {
    }
}
