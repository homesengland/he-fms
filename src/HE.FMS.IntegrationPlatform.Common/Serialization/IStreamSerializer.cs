namespace HE.FMS.IntegrationPlatform.Common.Serialization;

public interface IStreamSerializer
{
    Task<T> Deserialize<T>(Stream stream, CancellationToken cancellationToken);
}
