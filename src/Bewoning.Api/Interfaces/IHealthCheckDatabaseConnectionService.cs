namespace Bewoning.Api.Interfaces;

public interface IHealthCheckDatabaseConnectionService
{
    Task<int> CheckDatabaseConnection();
}
