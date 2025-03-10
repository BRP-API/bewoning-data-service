using System.Globalization;
using Bewoning.Api.ApiModels.Bewoning;
using Bewoning.Api.Util;
using Bewoning.Api.ApiModels.PersonenHistorieBase;
using Bewoning.Api.Validation;
using Bewoning.Data.DatabaseModels;
using Bewoning.Data.Helpers;
using Bewoning.Data.Mappers.Helpers;

namespace Bewoning.Data.Mappers;
public interface IRvIGDataBewoningenMapper
{
    GbaBewoning? MapBewoning(List<(bewoning_bewoner dbBewoner, long plId)> dbBewonersPlIds, List<(bewoning_bewoner dbBewoner, long plId)> dbMogelijkeBewonersPlIds, string? verblijf_plaats_ident_code, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null);
    IEnumerable<GbaBewoning> MapMedebewoner(IEnumerable<(BewoningOnBsnWrapper bewoningOnBsnWrapper, IEnumerable<(lo3_adres adres, lo3_pl_verblijfplaats verblijfplaats)> adressenVerblijfplaatsen, IEnumerable<lo3_pl_persoon> medebewoners)> bewoningen);
    bool IsBewonerAfterPeildatum((bewoning_bewoner dbBewoner, long plId) dbBewonerPlId, DateTime? peildatum);
    bool IsBewonerBetweenDates((bewoning_bewoner dbBewoner, long plId) dbBewonerPlId, DateTime? datumVan, DateTime? datumTot);
    (List<(bewoning_bewoner dbBewoner, long plId)> bewoners, List<(bewoning_bewoner dbBewoner, long plId)> mogelijkeBewoners) FilterBewonersByPeildatum(IEnumerable<(bewoning_bewoner dbBewoner, long plId)> dbBewonersPlIds, DateTime? peildatum);
    (List<(bewoning_bewoner dbBewoner, long plId)> bewoners, List<(bewoning_bewoner dbBewoner, long plId)> mogelijkeBewoners) FilterBewonersByDatumVanDatumTot(IEnumerable<(bewoning_bewoner dbBewoner, long plId)> dbBewonersPlIds, DateTime? van, DateTime? tot);
}

public class RvIGDataBewoningenMapper : RvIGDataMapperBase, IRvIGDataBewoningenMapper
{
    public RvIGDataBewoningenMapper(IDomeinTabellenHelper domeinTabellenHelper) : base(domeinTabellenHelper) { }
    public static bool IsPeildatumBetweenStartAndEndDates(DateTime? peildatum, string? startString, string? endString)
    {
        var start = CreateDatumOnvolledig(startString);
        var end = CreateDatumOnvolledig(endString);

        var appliesOnStart = peildatum == null || start?.IsBefore(peildatum.Value) != false || start.IsOn(peildatum.Value);
        var appliesOnEnd = peildatum == null || end?.IsAfter(peildatum.Value) != false || end.OnlyYearHasValue() && end.IsOn(peildatum.Value);

        return appliesOnStart && appliesOnEnd;
    }
    public static DatumOnvolledig? CreateDatumOnvolledig(string? date) => !string.IsNullOrWhiteSpace(date) ? new DatumOnvolledig(date) : null;
    public GbaBewoning? MapBewoning(List<(bewoning_bewoner dbBewoner, long plId)> dbBewonersPlIds, List<(bewoning_bewoner dbBewoner, long plId)> dbMogelijkeBewonersPlIds, string? verblijf_plaats_ident_code, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null)
    {
        GbaBewoning? bewoning = null;
        List<(bewoning_bewoner dbBewoner, long plId)>? opgeschorteBewoners = dbBewonersPlIds
                .Where(dbBewonerPlId => !IsBewonerOpgeschort(dbBewonerPlId.dbBewoner.pl_bijhouding_opschort_reden, dbBewonerPlId.dbBewoner.pl_bijhouding_opschort_datum, peildatum, van, tot))
                .ToList();
        List<(GbaBewoner gbaBewoner, long plId)>? bewoners = opgeschorteBewoners
            .ConvertAll(dbBewonerPlId => (new GbaBewoner
            {
                Burgerservicenummer = dbBewonerPlId.dbBewoner.burger_service_nr?.ToString().PadLeft(9, '0'),
                GeheimhoudingPersoonsgegevens = dbBewonerPlId.dbBewoner.pl_geheim_ind.HasValue && dbBewonerPlId.dbBewoner.pl_geheim_ind != 0 ? dbBewonerPlId.dbBewoner.pl_geheim_ind : null,
                VerblijfplaatsInOnderzoek = MapGbaInOnderzoek(dbBewonerPlId.dbBewoner.vb_onderzoek_gegevens_aand, dbBewonerPlId.dbBewoner.vb_onderzoek_start_datum, dbBewonerPlId.dbBewoner.vb_onderzoek_eind_datum),
                Geboorte = MapBewonerGeboorte(dbBewonerPlId.dbBewoner),
                Naam = MapBewonerNaam(dbBewonerPlId.dbBewoner).GetAwaiter().GetResult(),
                Geslacht = GbaMappingHelper.ParseToGeslachtEnum(dbBewonerPlId.dbBewoner.geslachts_aand)
            }, dbBewonerPlId.plId))
;
        List<(bewoning_bewoner dbBewoner, long plId)>? opgeschorteMogelijkeBewoners = dbMogelijkeBewonersPlIds
            .Where(dbBewonerPlId => !IsBewonerOpgeschort(dbBewonerPlId.dbBewoner.pl_bijhouding_opschort_reden, dbBewonerPlId.dbBewoner.pl_bijhouding_opschort_datum, peildatum, van, tot))
            .ToList();
        List<(GbaBewoner gbaBewoner, long plId)>? mogelijkeBewoners = opgeschorteMogelijkeBewoners
            .ConvertAll(dbMogelijkeBewonerPlId => (new GbaBewoner
            {
                Burgerservicenummer = dbMogelijkeBewonerPlId.dbBewoner.burger_service_nr?.ToString().PadLeft(9, '0'),
                GeheimhoudingPersoonsgegevens = dbMogelijkeBewonerPlId.dbBewoner.pl_geheim_ind.HasValue && dbMogelijkeBewonerPlId.dbBewoner.pl_geheim_ind != 0 ? dbMogelijkeBewonerPlId.dbBewoner.pl_geheim_ind : null,
                VerblijfplaatsInOnderzoek = MapGbaInOnderzoek(dbMogelijkeBewonerPlId.dbBewoner.vb_onderzoek_gegevens_aand, dbMogelijkeBewonerPlId.dbBewoner.vb_onderzoek_start_datum, dbMogelijkeBewonerPlId.dbBewoner.vb_onderzoek_eind_datum),
                Geboorte = MapBewonerGeboorte(dbMogelijkeBewonerPlId.dbBewoner),
                Naam = MapBewonerNaam(dbMogelijkeBewonerPlId.dbBewoner).GetAwaiter().GetResult(),
                Geslacht = GbaMappingHelper.ParseToGeslachtEnum(dbMogelijkeBewonerPlId.dbBewoner.geslachts_aand)
            }, dbMogelijkeBewonerPlId.plId))
;

        if (bewoners?.Count > 0 || mogelijkeBewoners?.Count > 0)
        {
            var totalBewonerCount = bewoners?.Count + mogelijkeBewoners?.Count;
            bewoning = new GbaBewoning
            {
                Bewoners = totalBewonerCount <= 100 ? bewoners?.ConvertAll(bewoner => bewoner.gbaBewoner) : Enumerable.Empty<GbaBewoner>().ToList(),
                MogelijkeBewoners = totalBewonerCount <= 100 ? mogelijkeBewoners?.ConvertAll(bewoner => bewoner.gbaBewoner) : Enumerable.Empty<GbaBewoner>().ToList(),
                IndicatieVeelBewoners = totalBewonerCount > 100 ? true : null,

                // Used for protocollering. DO NOT SERIALIZE AS PART OF THE API RESPONSE.
                BewonersPlIds = totalBewonerCount <= 100 ? bewoners : null,
                MogelijkeBewonersPlIds = totalBewonerCount <= 100 ? mogelijkeBewoners : null,
                AdresseerbaarObjectIdentificatie = dbBewonersPlIds.Count > 0
                ? dbBewonersPlIds.Aggregate((bewoner1, bewoner2) => bewoner1.dbBewoner.vb_adreshouding_start_datum > bewoner2.dbBewoner.vb_adreshouding_start_datum ? bewoner1 : bewoner2).dbBewoner.adres_verblijf_plaats_ident_code
                : verblijf_plaats_ident_code
            };

            if (!peildatum.HasValue && (van.HasValue || tot.HasValue))
            {
                bewoning.Periode = new Periode
                {
                    DatumVan = van?.ToString("yyyy-MM-dd"),
                    DatumTot = tot?.ToString("yyyy-MM-dd")
                };
            }
        }

        return bewoning;
    }

    private async Task<GbaNaamBasis?> MapBewonerNaam(bewoning_bewoner dbBewoner)
    {
        var naam = new GbaNaamBasis
        {
            Voornamen = dbBewoner.diak_voor_naam ?? dbBewoner.voor_naam,
            Voorvoegsel = dbBewoner.geslachts_naam_voorvoegsel,
            Geslachtsnaam = dbBewoner.diak_geslachts_naam ?? dbBewoner.geslachts_naam
        };

        if (!string.IsNullOrEmpty(dbBewoner.titel_predicaat))
        {
            var adellijkeTitelPredicaatOmschrijvingSoort = await _domeinTabellenHelper.GetAdellijkeTitelPredikaatOmschrijvingEnSoort(dbBewoner.titel_predicaat);
            naam.AdellijkeTitelPredicaat = new AdellijkeTitelPredicaatType
            {
                Code = dbBewoner.titel_predicaat,
                Omschrijving = adellijkeTitelPredicaatOmschrijvingSoort.omschrijving,
                Soort = GbaMappingHelper.ParseToSoortAdellijkeTitelPredikaatEnum(adellijkeTitelPredicaatOmschrijvingSoort.soort)
            };
        }
        return naam.Geslachtsnaam != null || naam.AdellijkeTitelPredicaat != null || naam.Voornamen != null || naam.Voorvoegsel != null ? naam : null;
    }

    private GbaGeboorteBeperkt? MapBewonerGeboorte(bewoning_bewoner dbBewoner)
    {
        var geboorte = new GbaGeboorteBeperkt
        {
            Datum = dbBewoner.geboorte_datum.ToString()
        };

        return !string.IsNullOrEmpty(dbBewoner.geboorte_datum.ToString()) ? geboorte : null;
    }

    public (List<(bewoning_bewoner dbBewoner, long plId)> bewoners, List<(bewoning_bewoner dbBewoner, long plId)> mogelijkeBewoners) FilterBewonersByPeildatum(IEnumerable<(bewoning_bewoner dbBewoner, long plId)> dbBewonersPlIds, DateTime? peildatum)
    {
        var bewoners = new List<(bewoning_bewoner dbBewoner, long plId)>();
        var mogelijkeBewoners = new List<(bewoning_bewoner dbBewoner, long plId)>();

        dbBewonersPlIds.ToList().ForEach(bewoner =>
        {
            var isMogelijkeBewonerByInOnderzoek = GbaMappingHelper.IsMogelijkeBewonerByInOnderzoek(peildatum, peildatum!.Value.AddDays(1), bewoner.dbBewoner.vb_onderzoek_gegevens_aand, bewoner.dbBewoner.vb_onderzoek_start_datum.ToString(), bewoner.dbBewoner.vb_onderzoek_eind_datum.ToString(), bewoner.dbBewoner.vb_adreshouding_start_datum.ToString()!, bewoner.dbBewoner.vorige_start_adres_datum.ToString(), bewoner.dbBewoner.volgende_start_adres_datum.ToString());
            if (!isMogelijkeBewonerByInOnderzoek && (peildatum.HasValue && bewoner.dbBewoner.vb_adreshouding_start_datum?.ToString()?.Equals(peildatum?.ToString("yyyyMMdd")) == true || GbaMappingHelper.IsDatumOnvolledigBeforeOrOnPeildatum(bewoner.dbBewoner.vb_adreshouding_start_datum.ToString(), peildatum)
            && !GbaMappingHelper.IsDatumOnvolledigPotentiallyOnOrAfterPeildatum(bewoner.dbBewoner.volgende_start_adres_datum.ToString(), peildatum)))
            {
                bewoners.Add((bewoner.dbBewoner, bewoner.plId));
            }
            else if (isMogelijkeBewonerByInOnderzoek || peildatum.HasValue && GbaMappingHelper.IsPotentiallyBewoner(bewoner.dbBewoner.vb_adreshouding_start_datum.ToString(), bewoner.dbBewoner.vorige_start_adres_datum.ToString(), bewoner.dbBewoner.volgende_start_adres_datum.ToString(), peildatum, peildatum.Value.AddDays(1)))
            {
                mogelijkeBewoners.Add((bewoner.dbBewoner, bewoner.plId));
            }
        });

        return (bewoners, mogelijkeBewoners);
    }

    public (List<(bewoning_bewoner dbBewoner, long plId)> bewoners, List<(bewoning_bewoner dbBewoner, long plId)> mogelijkeBewoners) FilterBewonersByDatumVanDatumTot(IEnumerable<(bewoning_bewoner dbBewoner, long plId)> dbBewonersPlIds, DateTime? van, DateTime? tot)
    {
        var bewoners = new List<(bewoning_bewoner dbBewoner, long plId)>();
        var mogelijkeBewoners = new List<(bewoning_bewoner dbBewoner, long plId)>();

        dbBewonersPlIds.ToList().ForEach(bewoner =>
        {
            var isMogelijkeBewonerByInOnderzoek = GbaMappingHelper.IsMogelijkeBewonerByInOnderzoek(van, tot, bewoner.dbBewoner.vb_onderzoek_gegevens_aand, bewoner.dbBewoner.vb_onderzoek_start_datum.ToString(), bewoner.dbBewoner.vb_onderzoek_eind_datum.ToString(), bewoner.dbBewoner.vb_adreshouding_start_datum.ToString()!, bewoner.dbBewoner.vorige_start_adres_datum.ToString(), bewoner.dbBewoner.volgende_start_adres_datum.ToString());
            if (!isMogelijkeBewonerByInOnderzoek && GbaMappingHelper.IsDatumVolledigBetweenVanTot(bewoner.dbBewoner.vb_adreshouding_start_datum.ToString(), bewoner.dbBewoner.volgende_start_adres_datum.ToString(), van, tot))
            {
                bewoners.Add((bewoner.dbBewoner, bewoner.plId));
            }
            else if (isMogelijkeBewonerByInOnderzoek || GbaMappingHelper.IsPotentiallyBewoner(bewoner.dbBewoner.vb_adreshouding_start_datum.ToString(), bewoner.dbBewoner.vorige_start_adres_datum.ToString(), bewoner.dbBewoner.volgende_start_adres_datum.ToString(), van, tot))
            {
                mogelijkeBewoners.Add((bewoner.dbBewoner, bewoner.plId));
            }
        });

        return (bewoners, mogelijkeBewoners);
    }

    public bool IsBewonerAfterPeildatum((bewoning_bewoner dbBewoner, long plId) dbBewonerPlId, DateTime? peildatum) =>
        peildatum.HasValue && dbBewonerPlId.dbBewoner.vb_adreshouding_start_datum?.ToString()?.Equals(peildatum?.ToString("yyyyMMdd")) == true && dbBewonerPlId.dbBewoner.volgende_start_adres_datum?.Equals(0) == true || GbaMappingHelper.IsDatumOnvolledigBeforeOrOnPeildatum(dbBewonerPlId.dbBewoner.vb_adreshouding_start_datum.ToString(), peildatum)
            && !GbaMappingHelper.IsDatumOnvolledigPotentiallyOnOrAfterPeildatum(dbBewonerPlId.dbBewoner.volgende_start_adres_datum.ToString(), peildatum)
            || GbaMappingHelper.IsPeildatumBetweenTwoDatumOnvolledig(dbBewonerPlId.dbBewoner.vb_adreshouding_start_datum.ToString(), dbBewonerPlId.dbBewoner.volgende_start_adres_datum.ToString(), peildatum);

    public bool IsBewonerBetweenDates((bewoning_bewoner dbBewoner, long plId) dbBewonerPlId, DateTime? datumVan, DateTime? datumTot)
        => ValidationHelperBase.TimePeriodesOverlap(datumVan, datumTot, dbBewonerPlId.dbBewoner.vb_adreshouding_start_datum?.ToString(), dbBewonerPlId.dbBewoner.volgende_start_adres_datum?.ToString());

    private static bool IsBewonerOpgeschort(string? opschortingReden, int? opschortingDatum, DateTime? peildatum = null, DateTime? van = null, DateTime? tot = null)
    {
        if (string.IsNullOrEmpty(opschortingReden))
        {
            return false;
        }
        else if (opschortingReden.Equals("F") || opschortingReden.Equals("W"))
        {
            return true;
        }
        opschortingDatum = FixOnvolledigeDateInt(opschortingDatum, false);
        DateTime opschortingDateTime = DateTime.MinValue;
        if (opschortingDatum.HasValue)
        {
            DateTime.TryParseExact(opschortingDatum.Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out opschortingDateTime);
        }
        if (peildatum.HasValue && GbaMappingHelper.IsPeildatumBeforeDatumOnvolledig(opschortingDatum?.ToString(), peildatum))
        {
            return false;
        }
        else if (van.HasValue && tot.HasValue
            && !opschortingDateTime.Equals(DateTime.MinValue) && (van < opschortingDateTime || tot <= opschortingDateTime))
        {
            return false;
        }

        return true;
    }

    private static int? FixOnvolledigeDateInt(int? onvolledigeDateInt, bool isEndDate)
    {
        if (!onvolledigeDateInt.HasValue || onvolledigeDateInt.Value.Equals(0))
        {
            return null;
        }

        var year = string.Join("", onvolledigeDateInt.Value.ToString().Take(4));
        var month = string.Join("", onvolledigeDateInt.Value.ToString().Replace(year, "").Take(2));
        var day = string.Join("", onvolledigeDateInt.Value.ToString().Replace(year + month, "").Take(2));

        if (month == "00")
        {
            month = isEndDate ? "12" : "01";
        }
        if (day == "00")
        {
            day = isEndDate ? DateTime.DaysInMonth(int.Parse(year), int.Parse(month)).ToString() : "01";
        }

        return int.Parse(year + month + day);
    }

    public IEnumerable<GbaBewoning> MapMedebewoner(IEnumerable<(BewoningOnBsnWrapper bewoningOnBsnWrapper, IEnumerable<(lo3_adres adres, lo3_pl_verblijfplaats verblijfplaats)> adressenVerblijfplaatsen, IEnumerable<lo3_pl_persoon> medebewoners)> bewoningen)
    {
        var gbaBewoningen = new List<GbaBewoning>();
        foreach (var bewoning in bewoningen)
        {
            var gbaBewoning = new GbaBewoning
            {
                AdresseerbaarObjectIdentificatie = bewoning.bewoningOnBsnWrapper.verblijf_plaats_ident_code,
            };

            gbaBewoningen.Add(gbaBewoning);
        }

        if (gbaBewoningen?.Any() != true)
        {
            return new List<GbaBewoning>();
        }

        return gbaBewoningen;
    }
}