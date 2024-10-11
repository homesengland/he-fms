using System.Security.Claims;
using HE.FMS.Middleware.Common.Tests.Helpers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Common.Tests.Fakes;

public class FakeHttpRequestData(FunctionContext functionContext) : HttpRequestData(functionContext)
{
    public FakeHttpRequestData(object payload, FunctionContext functionContext)
        : this(functionContext)
    {
        Body = TestingHelper.SerializeToStream(payload);
    }

    public override Stream Body { get; } = new MemoryStream();

    public override HttpHeadersCollection Headers { get; } = [];

    public override IReadOnlyCollection<IHttpCookie> Cookies { get; }

    public override Uri Url { get; }

    public override IEnumerable<ClaimsIdentity> Identities { get; }

    public override string Method { get; }

    public override HttpResponseData CreateResponse()
    {
        return new FakeHttpResponseData(FunctionContext);
    }
}
