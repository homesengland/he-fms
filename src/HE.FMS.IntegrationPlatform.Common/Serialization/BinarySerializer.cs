using System.Text;
using System.Text.Json;
using HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Common.Serialization;

internal sealed class BinarySerializer : IBinarySerializer
{
    private readonly ILogger<BinarySerializer> _logger;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true, };

    public BinarySerializer(ILogger<BinarySerializer> logger)
    {
        _logger = logger;
    }

    public T Deserialize<T>(BinaryData binaryData)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(binaryData), _jsonSerializerOptions)
                   ?? throw new FailedSerializationException();
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Error during deserialization");
            throw new FailedSerializationException();
        }
    }
}
