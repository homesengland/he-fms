using System.Text.Json;
using System.Text.Json.Serialization;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using Microsoft.Extensions.Logging;
using MiniValidation;

namespace HE.FMS.Middleware.Common.Serialization;

internal sealed class StreamSerializer : IStreamSerializer
{
    private readonly ILogger<StreamSerializer> _logger;

    private readonly JsonSerializerOptions _serializerOptions;

    public StreamSerializer(JsonSerializerOptions serializerOptions, ILogger<StreamSerializer> logger)
    {
        _serializerOptions = serializerOptions;
        _logger = logger;
    }

    public async Task<T> Deserialize<T>(Stream stream, CancellationToken cancellationToken)
    {
        return await Deserialize<T>(stream, _serializerOptions, cancellationToken);
    }

    public async Task<T> Deserialize<T>(Stream stream, JsonSerializerOptions serializerOptions, CancellationToken cancellationToken)
    {
        T requestModel;

        try
        {
            requestModel = await JsonSerializer.DeserializeAsync<T>(stream, serializerOptions, cancellationToken)
                           ?? throw new MissingRequestException();
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Error during http request deserialization");
            throw new FailedSerializationException(ex.Message);
        }

        if (!MiniValidator.TryValidate(requestModel, out var validationErrors))
        {
            throw new InvalidRequestException(validationErrors.SelectMany(x => x.Value.Select(y => $"{x.Key}: {y}")).ToArray());
        }

        return requestModel;
    }
}
