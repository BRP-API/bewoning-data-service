using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Bewoning.Api.Helpers;
using Bewoning.Api.Options;
using Bewoning.Data.DatabaseModels;
using Bewoning.Data.Repositories.Queries.Helper;
using Bewoning.Data.Repositories.Postgres;

namespace Bewoning.Data.Repositories.Bewoningen;
public interface IRvigBewoningenRepo
{
    Task<DbBewoningWrapper?> GetBewoningen(string adresseerbaarObjectIdentificatie);
    Task<IEnumerable<(BewoningOnBsnWrapper bewoningOnBsnWrapper, IEnumerable<(lo3_adres, lo3_pl_verblijfplaats)> adressenVerblijfplaatsen, IEnumerable<lo3_pl_persoon> medebewoners)>> GetMedebewoners(string bsn);
}

public class RvigBewoningenRepo : RvigRepoPostgresBase<bewoning_bewoner>, IRvigBewoningenRepo
{
    internal class AdresseerbaarObjectExists
    {
        public bool Exists { get; set; }
    }

    public RvigBewoningenRepo(IOptions<DatabaseOptions> databaseOptions, ILoggingHelper loggingHelper) : base(databaseOptions, loggingHelper)
    {
    }

    protected override void SetMappings() => CreateMappingsFromWhereMappings();

    protected override void SetWhereMappings() => WhereMappings = RvIGBewoningenWhereMappingsHelper.GetBewoningenBewonerMappings();

    public async Task<DbBewoningWrapper?> GetBewoningen(string adresseerbaarObjectIdentificatie)
    {
        return await GetBewoningByAdresseerbaarObjectIdentificatie(adresseerbaarObjectIdentificatie);
    }

    public async Task<IEnumerable<(BewoningOnBsnWrapper bewoningOnBsnWrapper, IEnumerable<(lo3_adres, lo3_pl_verblijfplaats)> adressenVerblijfplaatsen, IEnumerable<lo3_pl_persoon> medebewoners)>> GetMedebewoners(string bsn)
    {
        IEnumerable<BewoningOnBsnWrapper> bewoningOnBsnWrappers = await GetBewoningByBsn(bsn);

        var bewoningen = new List<(BewoningOnBsnWrapper bewoningOnBsnWrapper, IEnumerable<(lo3_adres, lo3_pl_verblijfplaats)> adressenVerblijfplaatsen, IEnumerable<lo3_pl_persoon> medebewoners)>();
        foreach (BewoningOnBsnWrapper bewoning in bewoningOnBsnWrappers)
        {
            if (string.IsNullOrWhiteSpace(bewoning.verblijf_plaats_ident_code))
            {
                break;
            }
            var aoIdWhere = BewoningenQueryHelper.CreateAdresseerbaarObjectIdentificatieWhereDapper(bewoning.verblijf_plaats_ident_code);
            var adressenVerblijfplaatsen = await ExecuteDapperQuery(aoIdWhere, BewoningenQueryHelper.BewoningBsnStap2Query, ExecuteBewoningBsnAdressenQuery);
            var medebewoners = await GetDataViaDapper<lo3_pl_persoon>(BewoningenQueryHelper.BewoningBsnStap3Query, aoIdWhere.Item2, aoIdWhere.Item1);

            bewoningen.Add((bewoning, adressenVerblijfplaatsen, medebewoners));
        }

        return bewoningen;
    }

    private async Task<DbBewoningWrapper> GetBewoningByAdresseerbaarObjectIdentificatie(string adresseerbaarObjectIdentificatie)
    {
        (string where, NpgsqlParameter parameter) = BewoningenQueryHelper.CreateAdresseerbaarObjectIdentificatieWhere(adresseerbaarObjectIdentificatie);
        var query = string.Format(BewoningenQueryHelper.BewonersByAoIdQuery, WhereMappings.Select(o => o.Key).Aggregate((i, j) => i + "," + j), where);
        var command = new NpgsqlCommand(query);
        command.Parameters.Add(parameter);

        var bewoningBewoners = (await GetFilterResultAsync(command)).ToList();

        if (bewoningBewoners.Count == 0)
        {
            return new DbBewoningWrapper
            {
                verblijf_plaats_ident_code = adresseerbaarObjectIdentificatie,
            };
        }

        var groupedBewoningen = bewoningBewoners.GroupBy(r => r.pl_id);

        foreach (var group in groupedBewoningen)
        {
            var orderedBewoningen = group.OrderByDescending(r => r.vb_volg_nr).ToList();

            for (int i = 0; i < orderedBewoningen.Count; i++)
            {
                var current = orderedBewoningen[i];
                var previous = i > 0 ? orderedBewoningen[i - 1] : null;
                var next = i < orderedBewoningen.Count - 1 ? orderedBewoningen[i + 1] : null;

                // Assign adres_datums
                current.vorige_start_adres_datum = previous?.vb_adreshouding_start_datum;
                current.volgende_start_adres_datum = next?.vb_adreshouding_start_datum;

                // Assign adres_ident_code
                current.vorige_adres_verblijf_plaats_ident_code = previous?.adres_verblijf_plaats_ident_code;
                current.volgende_adres_verblijf_plaats_ident_code = next?.adres_verblijf_plaats_ident_code;
            }
        }

        var finalBewoningBewoners = groupedBewoningen.SelectMany(grp => grp).ToList();

        return new DbBewoningWrapper
        {
            Bewoners = finalBewoningBewoners.Where(bewoningBewoner => !string.IsNullOrWhiteSpace(bewoningBewoner.vb_adres_functie) && !bewoningBewoner.vb_adres_functie.Equals("B", StringComparison.CurrentCultureIgnoreCase)),
            verblijf_plaats_ident_code = adresseerbaarObjectIdentificatie,
        };
    }

    private async Task<IEnumerable<BewoningOnBsnWrapper>> GetBewoningByBsn(string bsn)
    {
        var dbBewoningWrappers = await ExecuteDapperQuery(BewoningenQueryHelper.CreateBurgerservicenummerWhereDapper(bsn), BewoningenQueryHelper.BewoningBsnQuery, ExecuteBewoningBsnQuery);

        if (dbBewoningWrappers?.Any() != true)
        {
            return Enumerable.Empty<BewoningOnBsnWrapper>();
        }

        // list is already validated as not null or empty above and the where below already removes all null values. It is incorrect to view this as nullable at this point.
        return dbBewoningWrappers!.Where(bewoning => bewoning != null)!;
    }

    private Task<IEnumerable<BewoningOnBsnWrapper>> ExecuteBewoningBsnQuery(string query, DynamicParameters dynamicParameters)
    {
        var types = new[] { typeof(BewoningOnBsnWrapper) };

        return DapperQueryAsync(query, types, obj => MappingBewoningBsnFunction(obj), dynamicParameters);
    }

    private Task<IEnumerable<(lo3_adres, lo3_pl_verblijfplaats)>> ExecuteBewoningBsnAdressenQuery(string query, DynamicParameters dynamicParameters)
    {
        var types = new[] { typeof(lo3_adres), typeof(lo3_pl_verblijfplaats) };

        const string splitOn = $"{nameof(lo3_adres.postcode)},{nameof(lo3_pl_verblijfplaats.vertrek_land_adres_1)}";

        return DapperQueryAsync(query, types, obj => MappingBewoningBsnAdresFunction(obj), dynamicParameters, splitOn: splitOn);
    }

    private static (lo3_adres, lo3_pl_verblijfplaats) MappingBewoningBsnAdresFunction(object[] obj)
    {
        lo3_adres adres = (lo3_adres)obj[0] ?? new lo3_adres();
        lo3_pl_verblijfplaats verblijfplaats = (lo3_pl_verblijfplaats)obj[1] ?? new lo3_pl_verblijfplaats();
        return (adres, verblijfplaats);
    }

    private static BewoningOnBsnWrapper MappingBewoningBsnFunction(object[] obj)
    {
        return (BewoningOnBsnWrapper)obj[0];
    }
}