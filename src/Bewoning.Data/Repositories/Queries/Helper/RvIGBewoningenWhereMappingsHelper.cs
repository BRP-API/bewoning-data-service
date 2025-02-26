using Bewoning.Data.DatabaseModels;

namespace Bewoning.Data.Repositories.Queries.Helper;

public class RvIGBewoningenWhereMappingsHelper : RvIGBaseWhereMappingsHelper
{
    public static IDictionary<string, string> GetBewoningenBewonerMappings() => new Dictionary<string, string>()
    {
        ["pers.pl_id"] = nameof(bewoning_bewoner.pl_id),
        ["pers.burger_service_nr"] = nameof(bewoning_bewoner.burger_service_nr),
        ["pers.voor_naam"] = nameof(bewoning_bewoner.voor_naam),
        ["pers.diak_voor_naam"] = nameof(bewoning_bewoner.diak_voor_naam),
        ["pers.titel_predicaat"] = nameof(bewoning_bewoner.titel_predicaat),
        ["pers.geslachts_naam_voorvoegsel"] = nameof(bewoning_bewoner.geslachts_naam_voorvoegsel),
        ["pers.geslachts_naam"] = nameof(bewoning_bewoner.geslachts_naam),
        ["pers.diak_geslachts_naam"] = nameof(bewoning_bewoner.diak_geslachts_naam),
        ["pers.geslachts_aand"] = nameof(bewoning_bewoner.geslachts_aand),
        ["pers.geboorte_datum"] = nameof(bewoning_bewoner.geboorte_datum),
        ["vb.inschrijving_gemeente_code as vb_inschrijving_gemeente_code"] = nameof(bewoning_bewoner.vb_inschrijving_gemeente_code),
        ["vb.onderzoek_gegevens_aand as vb_onderzoek_gegevens_aand"] = nameof(bewoning_bewoner.vb_onderzoek_gegevens_aand),
        ["vb.onderzoek_start_datum as vb_onderzoek_start_datum"] = nameof(bewoning_bewoner.vb_onderzoek_start_datum),
        ["vb.onderzoek_eind_datum as vb_onderzoek_eind_datum"] = nameof(bewoning_bewoner.vb_onderzoek_eind_datum),
        ["vb.volg_nr as vb_volg_nr"] = nameof(bewoning_bewoner.vb_volg_nr),
        ["pl.bijhouding_opschort_datum as pl_bijhouding_opschort_datum"] = nameof(bewoning_bewoner.pl_bijhouding_opschort_datum),
        ["pl.bijhouding_opschort_reden as pl_bijhouding_opschort_reden"] = nameof(bewoning_bewoner.pl_bijhouding_opschort_reden),
        ["pl.geheim_ind as pl_geheim_ind"] = nameof(bewoning_bewoner.pl_geheim_ind),
        ["vb.aangifte_adreshouding_oms as vb_aangifte_adreshouding_oms"] = nameof(bewoning_bewoner.vb_aangifte_adreshouding_oms),
        ["vb.adres_functie as vb_adres_functie"] = nameof(bewoning_bewoner.vb_adres_functie),
        ["COALESCE(vb.adreshouding_start_datum, vb.vertrek_datum) as huidig_start_adres_datum"] = nameof(bewoning_bewoner.vb_adreshouding_start_datum),
        ["adres.verblijf_plaats_ident_code as adres_verblijf_plaats_ident_code"] = nameof(bewoning_bewoner.adres_verblijf_plaats_ident_code),
        ["adres.nummer_aand_ident_code as adres_nummer_aand_ident_code"] = nameof(bewoning_bewoner.adres_nummer_aand_ident_code),
        //["adres.gemeente_code as adres_gemeente_code"] = nameof(bewoning_bewoner.adres_gemeente_code),
        //["lead(COALESCE(vb.adreshouding_start_datum, vb.vertrek_datum)) over (PARTITION BY vb.pl_id order by vb.pl_id) as vorige_start_adres_datum"] = nameof(bewoning_bewoner.vorige_start_adres_datum),
        //["lag(COALESCE(vb.adreshouding_start_datum, vb.vertrek_datum)) over (PARTITION BY vb.pl_id order by vb.pl_id) as volgende_start_adres_datum"] = nameof(bewoning_bewoner.volgende_start_adres_datum),
        //["lead(adres.verblijf_plaats_ident_code) over (PARTITION BY vb.pl_id order by vb.pl_id) as vorige_adres_verblijf_plaats_ident_code"] = nameof(bewoning_bewoner.vorige_adres_verblijf_plaats_ident_code),
    };

    public static IDictionary<string, string> GetBewoningenInnerMappings() => new Dictionary<string, string>()
    {
        ["adres.verblijf_plaats_ident_code"] = nameof(DbBewoningWrapper.verblijf_plaats_ident_code),

        //["pers.burger_service_nr"] = nameof(lo3_pl_reis_doc.pl_id),
        //["adres.nummer_aand_ident_code"] = nameof(lo3_pl_reis_doc.stapel_nr),
        //["adres.verblijf_plaats_ident_code"] = nameof(),
        //["coalesce(vb.adreshouding_start_datum, vb.vertrek_datum) as begin_date"] = nameof(),
        //["lag(coalesce(vb.adreshouding_start_datum, vb.vertrek_datum), 1) over(partition by vb.pl_id order by vb.volg_nr) as end_date"] = nameof(),
        //["adres.straat_naam"] = nameof(),
        //["adres.huis_nr"] = nameof(),
        //["adres.huis_letter"] = nameof(),
        //["adres.huis_nr_toevoeging"] = nameof(),
        //["adres.huis_nr_aand"] = nameof(),
        //["adres.postcode"] = nameof(),
        //["adres.woon_plaats_naam"] = nameof(),
        //["adres.gemeente_code"] = nameof(),
        //["adres.locatie_beschrijving"] = nameof(),
        //["vb.vertrek_land_code"] = nameof(),
        //["vb.vertrek_land_adres_1"] = nameof(),
        //["vb.vertrek_land_adres_2"] = nameof(),
        //["vb.vertrek_land_adres_3"] = nameof(),
        //["vb.adres_functie"] = nameof(),
        //["pl.geheim_ind"] = nameof(),
        //["count(1) over() as aantalbewoners"] = nameof(),
        //["@MAXIMUMAANTAL as maximumaantal"] = nameof(),
        //["ROW_NUMBER() OVER(ORDER BY coalesce(vb.adreshouding_start_datum, vb.vertrek_datum) DESC, coalesce(vb.adreshouding_start_datum, vb.vertrek_datum) DESC) AS teller"] = nameof(),
    };

    //CASE WHEN aantalbewoners <= maximumaantal then burger_service_nr ELSE NULL END burger_service_nr,
    //nummer_aand_ident_code, verblijf_plaats_ident_code,
    //CASE WHEN aantalbewoners <= maximumaantal then begin_date ELSE NULL END begin_date,
    //CASE WHEN aantalbewoners <= maximumaantal then end_date ELSE NULL END end_date,
    //straat_naam, huis_nr, huis_letter, huis_nr_toevoeging, postcode, huis_nr_aand, woon_plaats_naam, gemeente_code,
    //locatie_beschrijving, vertrek_land_code, vertrek_land_adres_1, vertrek_land_adres_2, vertrek_land_adres_3, adres_functie,
    //CASE WHEN aantalbewoners <= maximumaantal then geheim_ind ELSE NULL END geheim_ind,
    //CASE WHEN aantalbewoners <= maximumaantal then adres_functie ELSE NULL END adres_functie,
    //CASE WHEN aantalbewoners <= maximumaantal then aantalbewoners ELSE NULL END aantalbewoners,
    //CASE WHEN aantalbewoners <= maximumaantal then maximumaantal ELSE NULL END maximumaantal,
    //CASE WHEN aantalbewoners <= maximumaantal then 'false' WHEN teller <= maximumaantal THEN 'true' ELSE NULL END indicatieveelbewoners
}