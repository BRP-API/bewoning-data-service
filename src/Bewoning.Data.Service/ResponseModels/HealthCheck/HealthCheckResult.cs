namespace Bewoning.Data.Service.ResponseModels.HealthCheck;

public class HealthCheckResult
{
	public HealthCheckResult(int databaseAvailable)
	{
		DatabaseAvailable = databaseAvailable;
	}

	public int DatabaseAvailable { get; set; }
}
