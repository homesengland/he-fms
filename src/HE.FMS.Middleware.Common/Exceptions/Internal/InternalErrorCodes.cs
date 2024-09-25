namespace HE.FMS.Middleware.Common.Exceptions.Internal;
public static class InternalErrorCodes
{
    public static string MissingConfiguration => nameof(MissingConfiguration);

    public static string MisingCosmosDbItem => nameof(MisingCosmosDbItem);

    public static string InvalidEnvironment => nameof(InvalidEnvironment);
}
