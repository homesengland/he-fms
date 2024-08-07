using System.Text.Json;

namespace HE.FMS.Middleware.Common.Serialization;

public interface IStreamSerializer
{
    Task<T> Deserialize<T>(Stream stream, CancellationToken cancellationToken);

    Task<T> Deserialize<T>(Stream stream, JsonSerializerOptions serializerOptions, CancellationToken cancellationToken);
}
