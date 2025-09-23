using Bewoning.Data.Authorisation;
using System.Diagnostics.CodeAnalysis;

namespace Bewoning.Data.DatabaseModels;

[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Record must conform the tablename in SQL source. Ignore C# casing convention because of this.")]
public record bewoning_bewoner
{
    public long pl_id { get; set; }
    [RubriekCategory(1), RubriekElement("01.20")] public long? burger_service_nr { get; set; }
    [RubriekCategory(1), RubriekElement("02.10")] public string? voor_naam { get; set; }
    [RubriekCategory(1), RubriekElement("02.40")] public string? diak_voor_naam { get; set; }
    [RubriekCategory(1), RubriekElement("02.20")] public string? titel_predicaat { get; set; }
    [RubriekCategory(1), RubriekElement("02.30")] public string? geslachts_naam_voorvoegsel { get; set; }
    [RubriekCategory(1), RubriekElement("02.40")] public string? geslachts_naam { get; set; }
    [RubriekCategory(1), RubriekElement("02.40")] public string? diak_geslachts_naam { get; set; }
    [RubriekCategory(1), RubriekElement("03.10")] public int? geboorte_datum { get; set; }
    [RubriekCategory(1), RubriekElement("04.10")] public string? geslachts_aand { get; set; }
    [RubriekCategory(7), RubriekElement("67.10")] public int? pl_bijhouding_opschort_datum { get; set; }
    [RubriekCategory(7), RubriekElement("67.20")] public string? pl_bijhouding_opschort_reden { get; set; }
    [RubriekCategory(7), RubriekElement("70.10")] public short? pl_geheim_ind { get; set; }
    [RubriekCategory(8), RubriekElement("92.10")] public short? vb_inschrijving_gemeente_code { get; set; }
    [RubriekCategory(8), RubriekElement("10.10")] public string? vb_adres_functie { get; set; }
    [RubriekCategory(8), RubriekElement("10.30")] public int? vb_adreshouding_start_datum { get; set; }
    [RubriekCategory(8), RubriekElement("10.30")] public int? vorige_start_adres_datum { get; set; }
    [RubriekCategory(8), RubriekElement("10.30")] public int? volgende_start_adres_datum { get; set; }
    [RubriekCategory(8), RubriekElement("83.10")] public int? onderzoek_gegevens_aand { get; set; }
    [RubriekCategory(8), RubriekElement("83.10")] public int? vb_onderzoek_gegevens_aand { get; set; }
    [RubriekCategory(8), RubriekElement("83.20")] public int? onderzoek_start_datum { get; set; }
    [RubriekCategory(8), RubriekElement("83.20")] public int? vb_onderzoek_start_datum { get; set; }
    [RubriekCategory(8), RubriekElement("83.30")] public int? onderzoek_eind_datum { get; set; }
    [RubriekCategory(8), RubriekElement("83.30")] public int? vb_onderzoek_eind_datum { get; set; }
    [RubriekCategory(8), RubriekElement("11.80")] public string? adres_verblijf_plaats_ident_code { get; set; }
    [RubriekCategory(8), RubriekElement("11.80")] public string? vorige_adres_verblijf_plaats_ident_code { get; set; }
    [RubriekCategory(8), RubriekElement("11.80")] public string? volgende_adres_verblijf_plaats_ident_code { get; set; }
    [RubriekCategory(8), RubriekElement("11.90")] public string? adres_nummer_aand_ident_code { get; set; }
    [RubriekCategory(8), RubriekElement("72.10")] public string? vb_aangifte_adreshouding_oms { get; set; }
    [AlwaysAuthorized] public short vb_volg_nr { get; set; }
}