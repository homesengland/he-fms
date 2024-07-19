namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;

public sealed class MissingRequiredFieldException : ValidationException
{
    public MissingRequiredFieldException(string fieldName)
        : base(ValidationErrorCodes.MissingRequiredField, $"Required field {fieldName} is missing.")
    {
    }
}
