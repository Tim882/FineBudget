using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FineBudget.Healthcheck
{
    public class LivenessHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(HealthCheckResult.Healthy("Healthy"));
        }
    }
}

