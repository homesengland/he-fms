using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HE.FMS.Middleware.Common.Exceptions.Communication;
using HE.FMS.Middleware.Providers.KeyVault.Settings;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Providers.KeyVault;

public class KeyVaultSecretClient : IKeyVaultSecretClient
{
    private readonly SecretClient _client;

    private readonly ILogger<KeyVaultSecretClient> _logger;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public KeyVaultSecretClient(KeyVaultSettings settings, ILogger<KeyVaultSecretClient> logger)
    {
        _client = new SecretClient(new Uri(settings.Url), new DefaultAzureCredential());
        _logger = logger;
    }

    public KeyVaultSecretClient(SecretClient client, ILogger<KeyVaultSecretClient> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<string> Get(string secretName, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.GetSecretAsync(secretName, cancellationToken: cancellationToken);
            if (!response.HasValue)
            {
                throw new ExternalSystemSerializationException("KeyVault", $"No response returned from KeyVault when fetching {secretName} secret.");
            }

            return response.Value.Value;
        }
        catch (RequestFailedException ex)
        {
            throw new ExternalSystemCommunicationException("KeyVault", (HttpStatusCode)ex.Status, ex);
        }
    }

    public async Task<TSecret> Get<TSecret>(string secretName, CancellationToken cancellationToken)
    {
        var secret = await Get(secretName, cancellationToken);

        try
        {
            return JsonSerializer.Deserialize<TSecret>(secret, _jsonSerializerOptions)
                   ?? throw new ExternalSystemSerializationException("KeyVault");
        }
        catch (Exception ex) when (ex is JsonException or NotSupportedException or ArgumentNullException)
        {
            _logger.LogError(ex, "Secret {SecretName} cannot be deserialized.", secretName);

            throw new ExternalSystemSerializationException("KeyVault", null, ex);
        }
    }

    public async Task Set(string secretName, string secretValue, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.SetSecretAsync(secretName, secretValue, cancellationToken: cancellationToken);
            if (!response.HasValue)
            {
                throw new ExternalSystemSerializationException("KeyVault", $"No response returned from KeyVault when updating {secretName} secret.");
            }
        }
        catch (RequestFailedException ex)
        {
            throw new ExternalSystemCommunicationException("KeyVault", (HttpStatusCode)ex.Status, ex);
        }
    }

    public async Task Set<TSecret>(string secretName, TSecret secret, CancellationToken cancellationToken)
    {
        var secretValue = JsonSerializer.Serialize(secret, _jsonSerializerOptions);

        await Set(secretName, secretValue, cancellationToken);
    }
}
