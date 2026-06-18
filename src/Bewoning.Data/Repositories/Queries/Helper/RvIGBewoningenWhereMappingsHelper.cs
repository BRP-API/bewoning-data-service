using Bewoning.Data.DatabaseModels;

namespace Bewoning.Data.Repositories.Queries.Helper;

public static class RvIGBewoningenWhereMappingsHelper
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
        ["adres.nummer_aand_ident_code as adres_nummer_aand_ident_code"] = nameof(bewoning_bewoner.adres_nummer_aand_ident_code)
    };
}