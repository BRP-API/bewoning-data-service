using Dapper;
using Npgsql;

namespace Rvig.Data.Bewoningen.Repositories.Queries.Helper;

public class BewoningenQueryHelper : QueryBaseHelper
{
    public static string BewoningAoIdQueryNew => @"select
	{0}
from lo3_pl_persoon pers
join lo3_pl pl on pl.pl_id = pers.pl_id
join lo3_pl_verblijfplaats vb on vb.pl_id = pers.pl_id
	join lo3_adres adres on adres.adres_id = vb.adres_id
{1}";

    public static string BewonersByAoIdQuery => @"select distinct
	{0}
from lo3_pl_persoon pers
	join lo3_pl pl on pl.pl_id = pers.pl_id
	join lo3_pl_verblijfplaats vb on vb.pl_id = pers.pl_id
		left join lo3_adres adres on adres.adres_id = vb.adres_id
where vb.pl_id in
(
	select distinct pers.pl_id
	from lo3_pl_persoon pers
		join lo3_pl pl on pl.pl_id = pers.pl_id
		join lo3_pl_verblijfplaats vb on vb.pl_id = pers.pl_id
			left join lo3_adres adres on adres.adres_id = vb.adres_id
{1} and vb.onjuist_ind is null
) and vb.onjuist_ind is null and pers.persoon_type = 'P' and pers.volg_nr = 0";

    public static string AoByAoIdExistsQuery => @"select exists (select
	1
from lo3_pl_verblijfplaats vb
		join lo3_adres adres on adres.adres_id = vb.adres_id
{0})";











    // ALL UNDER THIS COMMENT IS OLD.

    public static string BewoningBewonersQuery => @"select
	{0}
from lo3_pl_persoon pers
join lo3_pl_verblijfplaats verblfpls on verblfpls.pl_id = pers.pl_id and verblfpls.volg_nr = 0
	left join lo3_pl pl on pl.pl_id = pers.pl_id
	left join lo3_pl_overlijden overlijden
		on pers.pl_id = overlijden.pl_id and overlijden.volg_nr = 0
 {1}";
    public static string BewoningBsnQuery => @"select
	pers.pl_id,
	adres.verblijf_plaats_ident_code,
	coalesce(vb.adreshouding_start_datum, vb.vertrek_datum) as begin_date,
	lag(coalesce(vb.adreshouding_start_datum, vb.vertrek_datum), 1) over (partition by vb.pl_id order by vb.volg_nr) as end_date
from lo3_pl_persoon pers
join lo3_pl_verblijfplaats vb on vb.pl_id = pers.pl_id
left join lo3_adres adres on adres.adres_id = vb.adres_id
join lo3_pl pl on pl.pl_id = pers.pl_id
{0}
order by
	vb.pl_id,
	vb.volg_nr;";
    public static string BewoningBsnStap2Query => @"select
	adres.postcode,
	adres.huis_nr,
	adres.huis_letter,
	adres.huis_nr_toevoeging,
	adres.huis_nr_aand,
	adres.open_ruimte_naam,
	adres.diak_open_ruimte_naam,
	adres.diak_straat_naam,
	adres.straat_naam,
	adres.gemeente_code,
	adres.diak_woon_plaats_naam,
	adres.woon_plaats_naam,
	adres.diak_locatie_beschrijving,
	adres.locatie_beschrijving,
	adres.nummer_aand_ident_code,
	adres.verblijf_plaats_ident_code,
	vb.vertrek_land_adres_1,
	vb.vertrek_land_adres_2,
	vb.vertrek_land_adres_3,
	vb.vertrek_land_code,
	inschrvng_plts.gemeente_naam as inschrijving_gemeente_naam,
	vertrk_land.land_naam as vertrek_land_naam
from lo3_adres adres
join lo3_pl_verblijfplaats vb on vb.adres_id = adres.adres_id
	left join lo3_gemeente inschrvng_plts
		on inschrvng_plts.gemeente_code = vb.inschrijving_gemeente_code
	left join lo3_land vertrk_land
				on vertrk_land.land_code = vb.vertrek_land_code
{0};";
    public static string BewoningBsnStap3Query => @"select * from (
	select pers.*,
		coalesce(vb.adreshouding_start_datum, vb.vertrek_datum) as begin_date,
		lag(coalesce(vb.adreshouding_start_datum, vb.vertrek_datum), 1) over (partition by vb.pl_id order by vb.volg_nr) as end_date
	from lo3_pl_persoon pers
		join lo3_pl pl on pl.pl_id = pers.pl_id
	join lo3_pl_verblijfplaats vb on vb.pl_id = pers.pl_id
	join lo3_adres adres on adres.adres_id = vb.adres_id
	{0} and persoon_type = 'P' and pers.stapel_nr = 0 and pers.volg_nr = 0 and pl.pl_blokkering_start_datum is null
) potentialMedebewoners
where (begin_date BETWEEN coalesce(null, 19940508) AND coalesce(null, 99991231)) OR
(end_date BETWEEN coalesce(null, 19940508) AND coalesce(null, 99991231)) OR
(begin_date <= coalesce(null, 19940508) AND end_date >= coalesce(null, 99991231))";

    public static (string, DynamicParameters) CreateBurgerservicenummerWhereDapper(string bsn)
    {
        //  and pl.pl_blokkering_start_datum is null
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("BSN", long.Parse(bsn));

        return ("where burger_service_nr = @BSN and persoon_type = 'P' and pers.stapel_nr = 0 and pers.volg_nr = 0 and((pl.bijhouding_opschort_reden is not null and pl.bijhouding_opschort_reden != 'W') or pl.bijhouding_opschort_reden is null)", dynamicParameters);
    }

    public static (string where, NpgsqlParameter pgsqlParam) CreateAdresseerbaarObjectIdentificatieWhere(string adresseerbaarObjectIdentificatie)
    {
        return ("where adres.verblijf_plaats_ident_code = @IDENTIFICATIECODEADRESSEERBAAROBJECT", new NpgsqlParameter("IDENTIFICATIECODEADRESSEERBAAROBJECT", adresseerbaarObjectIdentificatie));
    }

    public static (string, DynamicParameters) CreateAdresseerbaarObjectIdentificatieWhereDapper(string adresseerbaarObjectIdentificatie)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("IDENTIFICATIECODEADRESSEERBAAROBJECT", adresseerbaarObjectIdentificatie);

        return ("where adres.verblijf_plaats_ident_code = @IDENTIFICATIECODEADRESSEERBAAROBJECT", dynamicParameters);
    }
}
