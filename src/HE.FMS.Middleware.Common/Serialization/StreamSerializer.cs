using System.Text.Json;
using System.Text.Json.Serialization;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using Microsoft.Extensions.Logging;
using MiniValidation;

namespace HE.FMS.Middleware.Common.Serialization;

internal sealed class StreamSerializer : IStreamSerializer
{
    private readonly ILogger<StreamSerializer> _logger;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
    };

    public StreamSerializer(ILogger<StreamSerializer> logger)
    {
        _logger = logger;
    }

    public async Task<T> Deserialize<T>(Stream stream, CancellationToken cancellationToken)
    {
        T requestModel;

        try
        {
            requestModel = await JsonSerializer.DeserializeAsync<T>(stream, _jsonSerializerOptions, cancellationToken)
                           ?? throw new MissingRequestException();
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Error during http request deserialization");
            throw new FailedSerializationException();
        }

        if (!MiniValidator.TryValidate(requestModel, out var validationErrors))
        {
            throw new InvalidRequestException(validationErrors.SelectMany(x => x.Value.Select(y => $"{x.Key}: {y}")).ToArray());
        }

        return requestModel;
    }
}
