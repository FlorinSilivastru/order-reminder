using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace CustomerInventoryService.Infrastructure.Configuration.HealthChecks;

public class UserApiHealthCheck(ILogger<UserApiHealthCheck> logger) : IHealthCheck
{
    private readonly ILogger<UserApiHealthCheck> logger = logger;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            //call external service to check if available

            return HealthCheckResult.Healthy($"Enterprise integration check succeeded.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"User api integration check failed.");
            return new HealthCheckResult(context.Registration.FailureStatus, $"User api integration check failed!", ex);
        }
    }
}