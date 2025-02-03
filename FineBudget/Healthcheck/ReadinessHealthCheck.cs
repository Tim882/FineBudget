using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FineBudget.Healthcheck
{
    public class ReadinessHealthCheck : IHealthCheck
    {
        private readonly IEnumerable<IHealthCheck> _checks;

        public ReadinessHealthCheck(IEnumerable<IHealthCheck> checks)
        {
            _checks = checks;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var results = await Task.WhenAll(_checks.Select(c => c.CheckHealthAsync(context, cancellationToken)));

            if (results.Any(r => r.Status == HealthStatus.Unhealthy))
            {
                return HealthCheckResult.Unhealthy();
            }

            return HealthCheckResult.Healthy();
        }
    }
}

