namespace HE.FMS.IntegrationPlatform.Common.Serialization;

public interface IBinarySerializer
{
    T Deserialize<T>(BinaryData binaryData);
}
