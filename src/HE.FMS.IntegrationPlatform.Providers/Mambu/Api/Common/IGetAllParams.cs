namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common;

public interface IGetAllParams
{
    IEnumerable<KeyValuePair<string, string>> ToQueryParams();
}
