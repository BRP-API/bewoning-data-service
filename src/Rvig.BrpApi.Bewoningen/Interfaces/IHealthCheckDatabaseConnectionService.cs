namespace Rvig.BrpApi.Bewoningen.Interfaces;

public interface IHealthCheckDatabaseConnectionService
{
	Task<int> CheckDatabaseConnection();
}
