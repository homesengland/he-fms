namespace HE.FMS.Middleware.Common.Tests.Fakes;

[FakeTest("ClassAttributeValue")]
public class FakeAttributeClass
{
    [FakeTest("PropertyAttributeValue")]
    public string TestProperty { get; set; }
}
