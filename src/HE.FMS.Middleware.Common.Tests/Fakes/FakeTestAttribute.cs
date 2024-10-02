namespace HE.FMS.Middleware.Common.Tests.Fakes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class FakeTestAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}
