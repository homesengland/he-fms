namespace HE.FMS.Middleware.Common.Serialization;

public interface IStreamSerializer
{
    Task<T> Deserialize<T>(Stream stream, CancellationToken cancellationToken);
}
