namespace HE.FMS.Middleware.Providers.Mambu.Api.Common;

public interface IGetAllParams
{
    IEnumerable<KeyValuePair<string, string>> ToQueryParams();
}
