namespace HE.FMS.Middleware.Contract.Common;

public interface IItemSet
{
    string IdempotencyKey { get; }
}
