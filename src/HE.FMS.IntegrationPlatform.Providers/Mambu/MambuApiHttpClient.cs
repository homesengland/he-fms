using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu;

internal sealed class MambuApiHttpClient : IMambuApiHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<MambuApiHttpClient> _logger;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public MambuApiHttpClient(HttpClient httpClient, ILogger<MambuApiHttpClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<TResponse> Send<TRequest, TResponse>(HttpMethod httpMethod, string relativeUrl, TRequest requestBody, CancellationToken cancellationToken)
    {
        using var httpRequest = new HttpRequestMessage(httpMethod, relativeUrl)
        {
            Content = new StringContent(JsonSerializer.Serialize(requestBody, _jsonSerializerOptions), Encoding.UTF8, "application/json"),
        };

        return await SendAsync<TResponse>(httpRequest, cancellationToken);
    }

    private static string GetRequestDetails(HttpRequestMessage httpRequest)
    {
        return $"{httpRequest.Method} {httpRequest.RequestUri} Mambu request";
    }

    private async Task<TResponse> SendAsync<TResponse>(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
    {
        using var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        if (!httpResponse.IsSuccessStatusCode)
        {
            var errorContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("{Request} returned {StatusCode} with payload \"{ErrorContent}\".", GetRequestDetails(httpRequest), httpResponse.StatusCode, errorContent);

            throw new ExternalSystemCommunicationException("Mambu", httpResponse.StatusCode);
        }

        var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrEmpty(responseContent))
        {
            _logger.LogError("{Request} returned {StatusCode} without payload.", GetRequestDetails(httpRequest), httpResponse.StatusCode);
            throw new ExternalSystemSerializationException("Mambu");
        }

        try
        {
            return JsonSerializer.Deserialize<TResponse>(responseContent, _jsonSerializerOptions)
                   ?? throw new ExternalSystemSerializationException("Mambu");
        }
        catch (Exception ex) when (ex is JsonException or NotSupportedException or ArgumentNullException)
        {
            _logger.LogError(
                ex,
                "{Request} returned {StatusCode} but payload cannot be deserialized \"{Response}\".",
                GetRequestDetails(httpRequest),
                httpResponse.StatusCode,
                responseContent);

            throw new ExternalSystemSerializationException("Mambu", ex);
        }
    }
}
