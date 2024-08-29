using HE.FMS.Middleware.Shared.Extensions;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder().SetupHostBuilder();

await host.RunAsync();
