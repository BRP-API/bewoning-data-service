using Bewoning.Api.Interfaces;
using Bewoning.Data.Repositories.Postgres;

namespace Bewoning.Data.Services;

public class HealthCheckDatabaseConnectionService : IHealthCheckDatabaseConnectionService
{
    private readonly IRvigDbHealthCheckRepo _rvigDbHealthCheckRepo;

    public HealthCheckDatabaseConnectionService(IRvigDbHealthCheckRepo rvigDbHealthCheckRepo)
    {
        _rvigDbHealthCheckRepo = rvigDbHealthCheckRepo;
    }

    public async Task<int> CheckDatabaseConnection()
    {
        var result = await _rvigDbHealthCheckRepo.SendSimpleQuery();

        return !string.IsNullOrWhiteSpace(result) ? 0 : 1;
    }
}
