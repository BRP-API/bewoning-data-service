using Rvig.BrpApi.Bewoningen.Interfaces;
using Bewoning.Data.Service.ResponseModels.HealthCheck;

namespace Bewoning.Data.Service.Services;

public interface IHealthCheckApiService
{
    Task<HealthCheckResult> CheckConnections();
}

public class HealthCheckApiService : IHealthCheckApiService
{
    protected IHealthCheckDatabaseConnectionService _healthCheckDatabaseConnectionService;

    public HealthCheckApiService(IHealthCheckDatabaseConnectionService healthCheckDatabaseConnectionService)
    {
        _healthCheckDatabaseConnectionService = healthCheckDatabaseConnectionService;
    }

    public async Task<HealthCheckResult> CheckConnections()
    {
        var databaseAvailable = await _healthCheckDatabaseConnectionService.CheckDatabaseConnection();
        return new HealthCheckResult(databaseAvailable);
    }
}
