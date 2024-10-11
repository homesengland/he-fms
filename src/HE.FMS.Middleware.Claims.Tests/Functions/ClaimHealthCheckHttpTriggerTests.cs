using HE.FMS.Middleware.Claims.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Claims.Tests.Functions;
public class ClaimHealthCheckHttpTriggerTests
{
    private readonly HealthCheckService _healthCheckService;
    private readonly ILogger<ClaimHealthCheckHttpTrigger> _logger;
    private readonly ClaimHealthCheckHttpTrigger _function;

    public ClaimHealthCheckHttpTriggerTests()
    {
        _healthCheckService = Substitute.For<HealthCheckService>();
        _logger = Substitute.For<ILogger<ClaimHealthCheckHttpTrigger>>();
        _function = new ClaimHealthCheckHttpTrigger(_healthCheckService, _logger);
    }

    [Fact]
    public async Task Run_ShouldReturnHealthyStatus()
    {
        // Arrange
        var healthReport = new HealthReport(new Dictionary<string, HealthReportEntry>(), TimeSpan.Zero);
        _healthCheckService.CheckHealthAsync().Returns(healthReport);

        var request = Substitute.For<HttpRequest>();

        // Act
        var result = await _function.Run(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Healthy", okResult.Value);
        _logger.Received(1).LogInformation("Health check status: Healthy");
    }

    [Fact]
    public async Task Run_ShouldReturnUnhealthyStatus()
    {
        // Arrange
        var healthReport = new HealthReport(new Dictionary<string, HealthReportEntry>(), HealthStatus.Unhealthy, TimeSpan.Zero);
        _healthCheckService.CheckHealthAsync().Returns(healthReport);

        var request = Substitute.For<HttpRequest>();

        // Act
        var result = await _function.Run(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Unhealthy", okResult.Value);
        _logger.Received(1).LogInformation("Health check status: Unhealthy");
    }

    [Fact]
    public async Task Run_ShouldReturnDegradedStatus()
    {
        // Arrange
        var healthReport = new HealthReport(new Dictionary<string, HealthReportEntry>(), HealthStatus.Degraded, TimeSpan.Zero);
        _healthCheckService.CheckHealthAsync().Returns(healthReport);

        var request = Substitute.For<HttpRequest>();

        // Act
        var result = await _function.Run(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Degraded", okResult.Value);
        _logger.Received(1).LogInformation("Health check status: Degraded");
    }
}
