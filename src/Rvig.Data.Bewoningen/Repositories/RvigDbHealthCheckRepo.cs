using Microsoft.Extensions.Options;
using Rvig.BrpApi.Bewoningen.Helpers;
using Rvig.BrpApi.Bewoningen.Options;
using Rvig.Data.Bewoningen.DatabaseModels;

namespace Rvig.Data.Bewoningen.Repositories;

public interface IRvigDbHealthCheckRepo
{
    Task<string?> SendSimpleQuery();
}
public class RvigDbHealthCheckRepo : PostgresSqlQueryRepoBase<DbHealthCheckResult>, IRvigDbHealthCheckRepo
{
    public RvigDbHealthCheckRepo(IOptions<DatabaseOptions> databaseOptions, ILoggingHelper loggingHelper) : base(databaseOptions, loggingHelper)
    {
        SetWhereMappings();
        SetMappings();
    }

    protected override void SetMappings() => CreateMappingsFromWhereMappings();
    protected override void SetWhereMappings() => WhereMappings = new Dictionary<string, string>
    {
        ["'test' as selectresult"] = nameof(DbHealthCheckResult.SelectResult)
    };

    public async Task<string?> SendSimpleQuery()
    {
        var query = string.Format("select {0}", WhereMappings.Select(o => o.Key).Aggregate((i, j) => i + "," + j));
        var command = CreateDbCommand(query);

        try
        {
            var result = await GetFilterResultAsync(command);

            if (result?.Any() == true)
            {
                return result.Single().SelectResult;
            }

            return default;
        }
        catch
        {
            return default;
        }
    }
}