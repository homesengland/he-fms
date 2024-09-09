namespace HE.FMS.Middleware.Contract.Attributes.Efin;

[AttributeUsage(AttributeTargets.Class)]
public class EfinFileRowSizeAttribute(int rowSize = 0) : Attribute
{
    public int RowSize { get; init; } = rowSize;
}
