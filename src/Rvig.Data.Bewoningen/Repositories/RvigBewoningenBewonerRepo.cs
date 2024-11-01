using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Rvig.Data.Base.Postgres.DatabaseModels;
using Rvig.Data.Base.Postgres.Repositories;
using Rvig.Data.Bewoningen.DatabaseModels;
using Rvig.Data.Bewoningen.Repositories.Queries;
using Rvig.BrpApi.Shared.Helpers;
using Rvig.BrpApi.Shared.Options;
using System.Reflection.Metadata;

namespace Rvig.Data.Bewoningen.Repositories;

public interface IRvigBewoningenBewonerRepo
{
	Task<IEnumerable<bewoning_bewoner>> GetBewoners(IEnumerable<long> adresIds);
}

// DbBewoningWrapper is not correct.
public class RvigBewoningenBewonerRepo : RvigRepoPostgresBase<bewoning_bewoner>, IRvigBewoningenBewonerRepo
{
	public RvigBewoningenBewonerRepo(IOptions<DatabaseOptions> databaseOptions, ILoggingHelper loggingHelper) : base(databaseOptions, loggingHelper)
	{
	}

	protected override void SetMappings() => CreateMappingsFromWhereMappings();
	protected override void SetWhereMappings() => WhereMappings = RvIGBewoningenWhereMappingsHelper.GetBewoningenBewonerMappings();

	private Task<IEnumerable<bewoning_bewoner>> GetBewonersByAdresIds(IEnumerable<long> adresIds)
	{
		(string where, List<NpgsqlParameter> parameters) = GetAdresIdsWhere(adresIds, "verblfpls");
		var query = string.Format(BewoningenQueryHelper.BewoningBewonersQuery, WhereMappings.Select(o => o.Key).Aggregate((i, j) => i + "," + j), where);
		var command = new NpgsqlCommand(query);
		command.Parameters.AddRange(parameters.ToArray());

		return GetFilterResultAsync(command);
	}

	public Task<IEnumerable<bewoning_bewoner>> GetBewoners(IEnumerable<long> adresIds)
	{
		return GetBewonersByAdresIds(adresIds);
	}

	private static (string where, List<NpgsqlParameter> parameters) GetAdresIdsWhere(IEnumerable<long> adres_ids, string alias)
	{
		var persConditions = "and pers.persoon_type = 'P' and pers.stapel_nr = 0 and pers.volg_nr = 0 and ((pl.bijhouding_opschort_reden is not null and pl.bijhouding_opschort_reden != 'W') or pl.bijhouding_opschort_reden is null)";
		if (adres_ids.Count() == 1)
		{
			return ($"where {alias}.adres_id = @ADRES_ID {persConditions}", new List<NpgsqlParameter>
			{
				new NpgsqlParameter($"ADRES_ID", adres_ids.Single())
			});
		}
		else
		{
			(string where, List<NpgsqlParameter> parameters) whereStringAndParams = ("", new List<NpgsqlParameter>());
			var adresIdIndex = 0;
			var adresdIdConditions = adres_ids.Select(x =>
			{
				adresIdIndex++;
				whereStringAndParams.parameters.Add(new NpgsqlParameter($"ADRES_ID{adresIdIndex}", x));
				return $"@ADRES_ID{adresIdIndex}";
			});

			whereStringAndParams.where = $"where {alias}.adres_id in ({string.Join(", ", adresdIdConditions)}) {persConditions}";
			return whereStringAndParams;
		}
	}
}
