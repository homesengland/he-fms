namespace HE.FMS.Middleware.Common.Exceptions.Validation;

public abstract class ValidationException : Exception
{
    protected ValidationException(string code, string message)
        : base(message)
    {
        Code = code;
    }

    public string Code { get; }
}
