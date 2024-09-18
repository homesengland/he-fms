namespace HE.FMS.Middleware.Common.Exceptions.Internal;

public class MissingCosmosDbItemException : InternalSystemException
{
    public MissingCosmosDbItemException(string itemName)
        : base($"Required cosmos db item {itemName} is missing.")
    {
    }

    public override string ErrorCode => InternalErrorCodes.MisingCosmosDbItem;
}
