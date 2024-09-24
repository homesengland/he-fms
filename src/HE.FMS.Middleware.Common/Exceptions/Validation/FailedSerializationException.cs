namespace HE.FMS.Middleware.Common.Exceptions.Validation;

public sealed class FailedSerializationException : ValidationException
{
    public FailedSerializationException()
        : base(ValidationErrorCodes.FailedSerialization, "Error during request serialization")
    {
    }

    public FailedSerializationException(string message)
    : base(ValidationErrorCodes.FailedSerialization, message)
    {
    }
}
