namespace HE.FMS.Middleware.Common.Exceptions;

public class ErrorDto
{
    public ErrorDto(string errorCode, string? errorMessage = null)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public string ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }
}
