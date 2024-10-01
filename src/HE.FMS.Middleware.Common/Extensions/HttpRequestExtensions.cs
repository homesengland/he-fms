using HE.FMS.Middleware.Common.Exceptions.Validation;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Common.Extensions;
public static class HttpRequestExtensions
{
    public static string GetIdempotencyHeader(this HttpRequestData requestData)
    {
        return requestData.GetCustomHeader(Constants.CustomHeaders.IdempotencyKey);
    }

    public static string GetEnvironmentHeader(this HttpRequestData requestData)
    {
        return requestData.GetCustomHeader(Constants.CustomHeaders.Environment);
    }

    public static string GetCustomHeader(this HttpRequestData requestData, string headerName)
    {
        try
        {
            var headers = requestData.Headers.GetValues(headerName);

            if (headers.IsNullOrEmpty())
            {
                throw new MissingRequiredHeaderException(headerName);
            }

            if (headers.Count() > 1)
            {
                throw new InvalidRequestException($"Multiple '{headerName}' headers");
            }

            return headers.Single();

        }
        catch (InvalidOperationException ex)
        {
            throw new MissingRequiredHeaderException(headerName);
        }
    }
}
