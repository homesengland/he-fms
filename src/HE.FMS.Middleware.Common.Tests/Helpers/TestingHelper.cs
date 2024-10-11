namespace HE.FMS.Middleware.Common.Tests.Helpers;
public static class TestingHelper
{
    public static MemoryStream SerializeToStream(object o)
    {
        var stream = new MemoryStream();
        System.Text.Json.JsonSerializer.Serialize(stream, o);
        stream.Position = 0; // Reset stream position to the beginning
        return stream;
    }
}
