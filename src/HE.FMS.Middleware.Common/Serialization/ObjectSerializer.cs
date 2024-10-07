using System.Text.Json;
using HE.FMS.Middleware.Common.Exceptions.Validation;

namespace HE.FMS.Middleware.Common.Serialization;
public class ObjectSerializer : IObjectSerializer
{
    private readonly JsonSerializerOptions _serializerOptions;

    public ObjectSerializer(JsonSerializerOptions serializerOptions)
    {
        _serializerOptions = serializerOptions;
    }

    public string Serialize(object data)
    {
        return Serialize(data, _serializerOptions);
    }

    public string Serialize(object data, JsonSerializerOptions serializerOptions)
    {
        return JsonSerializer.Serialize(data, serializerOptions);
    }

    public T Deserialize<T>(string data)
    {
        return Deserialize<T>(data, _serializerOptions);
    }

    public T Deserialize<T>(string data, JsonSerializerOptions serializerOptions)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentNullException(nameof(data));
        }

        try
        {
            return JsonSerializer.Deserialize<T>(data, serializerOptions) ?? throw new FailedSerializationException();
        }
        catch (JsonException ex)
        {
            throw new FailedSerializationException(ex.Message);
        }
    }
}
