using System.Text.Json;

namespace HE.FMS.Middleware.Common.Serialization;
public interface IObjectSerializer
{
    string Serialize(object data);

    string Serialize(object data, JsonSerializerOptions serializerOptions);

    T Deserialize<T>(string data);

    T Deserialize<T>(string data, JsonSerializerOptions serializerOptions);
}
