using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;
using HE.FMS.IntegrationPlatform.Common.Serialization.Converters;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu;

internal abstract class MambuApiHttpClientBase
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<MambuApiHttpClientBase> _logger;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new EnumDescriptionJsonConverterFactory() },
    };

    protected MambuApiHttpClientBase(HttpClient httpClient, ILogger<MambuApiHttpClientBase> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    protected abstract string ApiName { get; }

    protected async Task Send(HttpMethod httpMethod, string relativeUrl, CancellationToken cancellationToken, Action<HttpRequestMessage>? httpRequestMessageBuilder = null)
    {
        using var httpRequest = new HttpRequestMessage(httpMethod, relativeUrl);
        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.mambu.v2+json"));

        httpRequestMessageBuilder?.Invoke(httpRequest);

        await SendAsync(httpRequest, cancellationToken);
    }

    protected async Task<TResponse> Send<TResponse>(HttpMethod httpMethod, string relativeUrl, CancellationToken cancellationToken, Action<HttpRequestMessage>? httpRequestMessageBuilder = null)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.mambu.v2+json"));

        httpRequestMessageBuilder?.Invoke(httpRequest);

        var httpResponse = await SendAsync(httpRequest, cancellationToken);

        return await ParseResponse<TResponse>(httpRequest, httpResponse, cancellationToken);
    }

    protected async Task<TResponse> Send<TRequest, TResponse>(HttpMethod httpMethod, string relativeUrl, TRequest requestBody, CancellationToken cancellationToken, Action<HttpRequestMessage>? httpRequestMessageBuilder = null)
    {
        using var httpRequest = new HttpRequestMessage(httpMethod, relativeUrl);
        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.mambu.v2+json"));
        httpRequest.Content = new StringContent(JsonSerializer.Serialize(requestBody, _jsonSerializerOptions), Encoding.UTF8, "application/json");

        httpRequestMessageBuilder?.Invoke(httpRequest);

        using var httpResponse = await SendAsync(httpRequest, cancellationToken);

        return await ParseResponse<TResponse>(httpRequest, httpResponse, cancellationToken);
    }

    private static string GetRequestDetails(HttpRequestMessage httpRequest)
    {
        return $"{httpRequest.Method} {httpRequest.RequestUri} Mambu request";
    }

    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
    {
        var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        if (!httpResponse.IsSuccessStatusCode)
        {
            var errorContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("{Request} returned {StatusCode} with payload \"{ErrorContent}\".", GetRequestDetails(httpRequest), httpResponse.StatusCode, errorContent);

            throw new ExternalSystemCommunicationException(ApiName, httpResponse.StatusCode);
        }

        return httpResponse;
    }

    private async Task<TResponse> ParseResponse<TResponse>(HttpRequestMessage httpRequest, HttpResponseMessage httpResponse, CancellationToken cancellationToken)
    {
        var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrEmpty(responseContent))
        {
            _logger.LogError("{Request} returned {StatusCode} without payload.", GetRequestDetails(httpRequest), httpResponse.StatusCode);
            throw new ExternalSystemSerializationException(ApiName);
        }

        try
        {
            return JsonSerializer.Deserialize<TResponse>(responseContent, _jsonSerializerOptions)
                   ?? throw new ExternalSystemSerializationException(ApiName);
        }
        catch (Exception ex) when (ex is JsonException or NotSupportedException or ArgumentNullException)
        {
            _logger.LogError(
                ex,
                "{Request} returned {StatusCode} but payload cannot be deserialized \"{Response}\".",
                GetRequestDetails(httpRequest),
                httpResponse.StatusCode,
                responseContent);

            throw new ExternalSystemSerializationException(ApiName, null, ex);
        }
    }
}
