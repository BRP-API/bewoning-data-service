using Dapper;
using Microsoft.Extensions.Options;
using Rvig.BrpApi.Bewoningen.Exceptions;
using Rvig.BrpApi.Bewoningen.Helpers;
using Rvig.BrpApi.Bewoningen.Options;
using Rvig.Data.Bewoningen.DatabaseModels;
using Rvig.Data.Bewoningen.Providers;
using Rvig.Data.Bewoningen.Repositories.Queries.Helper;

namespace Rvig.Data.Bewoningen.Repositories;
public interface IAutorisationRepo
{
    Task<DbAutorisatie?> GetByAfnemerCode(int afnemerCode);
}

public class AutorisationRepo : PostgresRepoBase, IAutorisationRepo
{
    public ICurrentDateTimeProvider _currentDateTimeProvider { get; set; }

    public AutorisationRepo(IOptions<DatabaseOptions> databaseOptions, ICurrentDateTimeProvider currentDateTimeProvider
        , ILoggingHelper loggingHelper) : base(databaseOptions, loggingHelper)
    {
        _currentDateTimeProvider = currentDateTimeProvider;
    }

    public async Task<DbAutorisatie?> GetByAfnemerCode(int afnemerCode)
    {
        var currentDate = _currentDateTimeProvider.Today();
        var todayString = $"{currentDate.Year}{currentDate.Month.ToString().PadLeft(2, '0')}{currentDate.Day.ToString().PadLeft(2, '0')}";
        var todayInt = int.Parse(todayString);

        var query = QueryBaseHelper.AutorisatieQuery;

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("AFNEMERCODE", afnemerCode);
        dynamicParameters.Add("TODAY", todayInt);

        var autorisaties = await DapperQueryAsync<DbAutorisatie>(query, dynamicParameters);

        if (autorisaties.Count() > 1)
        {
            throw new AuthorizationException($"Meer dan 1 autorisaties gevonden.");
        }

        return autorisaties.SingleOrDefault();
    }
}