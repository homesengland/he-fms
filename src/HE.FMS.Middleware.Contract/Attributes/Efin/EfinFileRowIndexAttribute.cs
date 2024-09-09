namespace HE.FMS.Middleware.Contract.Attributes.Efin;

[AttributeUsage(AttributeTargets.Property)]
public class EfinFileRowIndexAttribute(int startIndex = 0, int endIndex = 0) : Attribute
{
    public int StartIndex { get; init; } = startIndex;

    public int EndIndex { get; init; } = endIndex;
}
