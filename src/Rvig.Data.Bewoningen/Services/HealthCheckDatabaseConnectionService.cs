using Rvig.BrpApi.Bewoningen.Interfaces;
using Rvig.Data.Bewoningen.Repositories;

namespace Rvig.Data.Bewoningen.Services;

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
