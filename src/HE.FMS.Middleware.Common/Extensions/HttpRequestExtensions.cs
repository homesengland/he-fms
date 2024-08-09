using Azure.Core;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.VisualBasic;

namespace HE.FMS.Middleware.Common.Extensions;
public static class HttpRequestExtensions
{
    public static string GetIdempotencyHeader(this HttpRequestData requestData)
    {
        var headers = requestData.Headers.GetValues(Constants.HttpHeaders.IdempotencyKey);

        if (headers.IsNullOrEmpty())
        {
            throw new MissingRequiredHeaderException(Constants.HttpHeaders.IdempotencyKey);
        }

        if (headers.Count() > 1)
        {
            throw new InvalidRequestException($"Multiple '{Constants.HttpHeaders.IdempotencyKey}' headers");
        }

        return headers.Single();
    }
}
