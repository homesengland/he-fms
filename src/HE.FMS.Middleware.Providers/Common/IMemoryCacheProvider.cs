namespace HE.FMS.Middleware.Providers.Common;

public interface IMemoryCacheProvider<T>
    where T : class
{
    Task<T> GetValue(string key);

    void InvalidateKey(string key);
}
