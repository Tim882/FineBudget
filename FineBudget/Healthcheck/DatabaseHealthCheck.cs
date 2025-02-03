using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FineBudget.Healthcheck
{
	public class DatabaseHealthCheck: IHealthCheck
	{
        private readonly BudgetContext _context;

        public DatabaseHealthCheck(BudgetContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            bool canConnect = await _context.Database.CanConnectAsync(cancellationToken);

            if (!canConnect)
            {
                return HealthCheckResult.Unhealthy("Could not connect to database");
            }

            return HealthCheckResult.Healthy("App is ready");
        }
    }
}

