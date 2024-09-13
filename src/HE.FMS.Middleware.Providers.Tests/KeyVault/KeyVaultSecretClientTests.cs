using System.Net;
using System.Text.Json;
using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HE.FMS.Middleware.Common.Exceptions.Communication;
using HE.FMS.Middleware.Providers.KeyVault;
using HE.FMS.Middleware.Providers.KeyVault.Settings;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.KeyVault;

public class KeyVaultSecretClientTests
{
    private readonly SecretClient _secretClient;
    private readonly KeyVaultSecretClient _keyVaultSecretClient;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public KeyVaultSecretClientTests()
    {
        var settings = Substitute.For<IKeyVaultSettings>();
        settings.Url.Returns("https://fake-keyvault-url");
        var logger = Substitute.For<ILogger<KeyVaultSecretClient>>();
        _secretClient = Substitute.For<SecretClient>(new Uri(settings.Url), new DefaultAzureCredential());
        _keyVaultSecretClient = new KeyVaultSecretClient(_secretClient, logger);
    }

    [Fact]
    public async Task Get_ShouldReturnSecretValue()
    {
        // Arrange
        var secretName = "test-secret";
        var secretValue = "secret-value";
        var secret = SecretModel(secretName, secretValue);
        _secretClient.GetSecretAsync(Arg.Any<string>(), cancellationToken: Arg.Any<CancellationToken>()).Returns(secret);

        // Act
        var result = await _keyVaultSecretClient.Get(secretName, CancellationToken.None);

        // Assert
        Assert.Equal(secretValue, result);
    }

    [Fact]
    public async Task Get_ShouldThrowExternalSystemCommunicationException_OnRequestFailedException()
    {
        // Arrange
        var secretName = "test-secret";
        _secretClient.GetSecretAsync(secretName, cancellationToken: Arg.Any<CancellationToken>())
            .Returns(Task.FromException<Response<KeyVaultSecret>>(new ExternalSystemCommunicationException("test", HttpStatusCode.BadRequest)));

        // Act & Assert
        await Assert.ThrowsAsync<ExternalSystemCommunicationException>(() => _keyVaultSecretClient.Get(secretName, CancellationToken.None));
    }

    [Fact]
    public async Task GetTSecret_ShouldReturnDeserializedSecret()
    {
        // Arrange
        var secretName = "test-secret";
        var secretValue = $"{{\"Name\":\"Test\"}}";
        var secret = SecretModel(secretName, secretValue);
        _secretClient.GetSecretAsync(secretName, cancellationToken: Arg.Any<CancellationToken>()).Returns(secret);

        // Act
        var result = await _keyVaultSecretClient.Get<TestSecret>(secretName, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task GetTSecret_ShouldThrowExternalSystemSerializationException_OnJsonException()
    {
        // Arrange
        var secretName = "test-secret";
        var secretValue = "invalid-json";
        var secret = SecretModel(secretName, secretValue);
        _secretClient.GetSecretAsync(secretName, cancellationToken: Arg.Any<CancellationToken>()).Returns(secret);

        // Act & Assert
        await Assert.ThrowsAsync<ExternalSystemSerializationException>(() => _keyVaultSecretClient.Get<TestSecret>(secretName, CancellationToken.None));
    }

    [Fact]
    public async Task Set_ShouldSetSecretValue()
    {
        // Arrange
        var secretName = "test-secret";
        var secretValue = "secret-value";
        var response = SecretModel(secretName, secretValue);
        _secretClient.SetSecretAsync(secretName, secretValue, Arg.Any<CancellationToken>()).Returns(response);

        // Act
        await _keyVaultSecretClient.Set(secretName, secretValue, CancellationToken.None);

        // Assert
        await _secretClient.Received(1).SetSecretAsync(secretName, secretValue, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Set_ShouldThrowExternalSystemCommunicationException_OnRequestFailedException()
    {
        // Arrange
        var secretName = "test-secret";
        var secretValue = "secret-value";
        _secretClient.SetSecretAsync(Arg.Any<string>(), Arg.Any<string>(), cancellationToken: Arg.Any<CancellationToken>())
            .Returns(Task.FromException<Response<KeyVaultSecret>>(new RequestFailedException(1, "test")));

        // Act & Assert
        await Assert.ThrowsAsync<ExternalSystemCommunicationException>(() => _keyVaultSecretClient.Set(secretName, secretValue, CancellationToken.None));
    }

    [Fact]
    public async Task SetTSecret_ShouldSerializeAndSetSecretValue()
    {
        // Arrange
        var secretName = "test-secret";
        var secret = new TestSecret { Name = "Test" };
        var secretValue = JsonSerializer.Serialize(secret, _jsonSerializerOptions);
        var response = SecretModel(secretName, secretValue);
        _secretClient.SetSecretAsync(secretName, secretValue, Arg.Any<CancellationToken>()).Returns(response);

        // Act
        await _keyVaultSecretClient.Set(secretName, secret, CancellationToken.None);

        // Assert
        await _secretClient.Received(1).SetSecretAsync(secretName, secretValue, Arg.Any<CancellationToken>());
    }

    private static Response<KeyVaultSecret> SecretModel(string name, string value)
    {
        var secret = new KeyVaultSecret(name, value);
        return Response.FromValue(secret, Substitute.For<Response>());
    }

    private sealed class TestSecret
    {
        public string Name { get; init; }
    }
}
