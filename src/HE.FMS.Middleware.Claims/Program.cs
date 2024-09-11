using HE.FMS.Middleware.Shared.Extensions;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder().SetupHostBuilder<Program>();

await host.RunAsync();
