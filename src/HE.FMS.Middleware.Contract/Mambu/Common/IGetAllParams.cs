namespace HE.FMS.Middleware.Contract.Mambu.Common;

public interface IGetAllParams
{
    IEnumerable<KeyValuePair<string, string>> ToQueryParams();
}
