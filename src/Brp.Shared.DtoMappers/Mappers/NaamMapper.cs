using Brp.Shared.DtoMappers.BrpApiDtos;
using Brp.Shared.DtoMappers.Interfaces;

namespace Brp.Shared.DtoMappers.Mappers;

public static class NaamMapper
{
    public static string Achternaam(this INaamBasis? naam)
    {
        return naam != null &&
               !string.IsNullOrWhiteSpace(naam.Geslachtsnaam) &&
               naam.Geslachtsnaam != "."
            ? $"{naam.Voorvoegsel} {naam.Geslachtsnaam}".RemoveRedundantSpaces()
            : string.Empty;
    }

    public static NaamPersoonBeperkt? Map(this CommonDtos.NaamBasis? naam, CommonDtos.Waardetabel geslacht, BrpDtos.InOnderzoek? inOnderzoek)
    {
        return naam == null
            ? null
            : new NaamPersoonBeperkt
            {
                AdellijkeTitelPredicaat = naam.AdellijkeTitelPredicaat.Map(),
                Voorletters = naam.Voorletters(),
                Voornamen = naam.Voornamen,
                VolledigeNaam = naam.VolledigeNaam(geslacht),
                Voorvoegsel = naam.Voorvoegsel,
                Geslachtsnaam = naam.MapGeslachtsnaam(),
                InOnderzoek = inOnderzoek?.MapNaamPersoonBeperktInOnderzoek()
            };
    }

    public static NaamPersoon? Map(this BrpDtos.GbaNaamPersoon? naam, CommonDtos.Waardetabel geslacht, BrpDtos.InOnderzoek? inOnderzoek)
    {
        return naam != null || inOnderzoek != null
            ? new NaamPersoon
            {
              AanduidingNaamgebruik = naam?.AanduidingNaamgebruik.Map(),
              AdellijkeTitelPredicaat = naam?.AdellijkeTitelPredicaat.Map(),
              Voorletters = naam?.Voorletters(),
              Voornamen = naam?.Voornamen,
              VolledigeNaam = naam?.VolledigeNaam(geslacht),
              Voorvoegsel = naam?.Voorvoegsel,
              Geslachtsnaam = naam.MapGeslachtsnaam(),
              Partners = naam?.Partners?.Map(),
              InOnderzoek = inOnderzoek?.MapNaamPersoonInOnderzoek()
            }
            : null;
    }

    private static BrpApiDtos.NaamPersoonInOnderzoekBeperkt? MapNaamPersoonBeperktInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        return source?.AanduidingGegevensInOnderzoek switch
        {
            "010000" => new BrpApiDtos.NaamPersoonInOnderzoekBeperkt
            {
                Voornamen = true,
                AdellijkeTitelPredicaat = true,
                Voorvoegsel = true,
                Geslachtsnaam = true,
                Voorletters = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010200" => new BrpApiDtos.NaamPersoonInOnderzoekBeperkt
            {
                Voornamen = true,
                AdellijkeTitelPredicaat = true,
                Voorvoegsel = true,
                Geslachtsnaam = true,
                Voorletters = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010210" => new BrpApiDtos.NaamPersoonInOnderzoekBeperkt
            {
                Voornamen = true,
                Voorletters = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010220" => new BrpApiDtos.NaamPersoonInOnderzoekBeperkt
            {
                AdellijkeTitelPredicaat = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010230" => new BrpApiDtos.NaamPersoonInOnderzoekBeperkt
            {
                Voorvoegsel = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010240" => new BrpApiDtos.NaamPersoonInOnderzoekBeperkt
            {
                Geslachtsnaam = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010400" or
            "010410" => new BrpApiDtos.NaamPersoonInOnderzoekBeperkt
            {
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null
        };
    }

    private static BrpApiDtos.NaamPersoonInOnderzoek? MapNaamPersoonInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        return source?.AanduidingGegevensInOnderzoek switch
        {
            "010000" => new BrpApiDtos.NaamPersoonInOnderzoek
            {
                Voornamen = true,
                AdellijkeTitelPredicaat = true,
                Voorvoegsel = true,
                Geslachtsnaam = true,
                AanduidingNaamgebruik = true,
                Voorletters = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010200" => new BrpApiDtos.NaamPersoonInOnderzoek
            {
                Voornamen = true,
                AdellijkeTitelPredicaat = true,
                Voorvoegsel = true,
                Geslachtsnaam = true,
                Voorletters = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010210" => new BrpApiDtos.NaamPersoonInOnderzoek
            {
                Voornamen = true,
                Voorletters = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010220" => new BrpApiDtos.NaamPersoonInOnderzoek
            {
                AdellijkeTitelPredicaat = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010230" => new BrpApiDtos.NaamPersoonInOnderzoek
            {
                Voorvoegsel = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010240" => new BrpApiDtos.NaamPersoonInOnderzoek
            {
                Geslachtsnaam = true,
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "010400" or "010410" => new BrpApiDtos.NaamPersoonInOnderzoek
            {
                VolledigeNaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "016100" or "016110" => new BrpApiDtos.NaamPersoonInOnderzoek
            {
                AanduidingNaamgebruik = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null
        };
    }

    public static NaamVolledigeNaam? MapNaamVolledigeNaam(this CommonDtos.NaamBasis? naam, CommonDtos.Waardetabel geslacht)
    {
        return naam == null
            ? null
            : new NaamVolledigeNaam
            {
                VolledigeNaam = naam.VolledigeNaam(geslacht)
            };
    }
}
