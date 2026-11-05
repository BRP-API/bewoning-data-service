using Brp.Shared.DtoMappers.BrpApiDtos;

namespace Brp.Shared.DtoMappers.Mappers;

public static class NaamGerelateerdeMapper
{
    public static NaamGerelateerde? MapNaamGerelateerde(this CommonDtos.NaamBasis? naam, BrpDtos.InOnderzoek inOnderzoek)
    {
        return naam == null && inOnderzoek == null
            ? null
            : new NaamGerelateerde
            {
                AdellijkeTitelPredicaat = naam?.AdellijkeTitelPredicaat.Map(),
                Voorletters = naam?.Voorletters(),
                Voornamen = naam?.Voornamen,
                Voorvoegsel = naam?.Voorvoegsel,
                Geslachtsnaam = naam.MapGeslachtsnaam(),
                InOnderzoek = inOnderzoek.MapNaamGerelateerdeInOnderzoek()
            };
    }

    public static string? MapGeslachtsnaam(this CommonDtos.NaamBasis? naam) =>
        naam?.Geslachtsnaam == "." ? null : naam?.Geslachtsnaam;

    public static BrpApiDtos.NaamInOnderzoek? MapNaamGerelateerdeInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        return source?.AanduidingGegevensInOnderzoek switch
        {
            "020000" or
            "020200" or
            "030000" or
            "030200" or
            "050000" or
            "050200" or
            "090000" or
            "090200" => new BrpApiDtos.NaamInOnderzoek
            {
                Voornamen = true,
                AdellijkeTitelPredicaat = true,
                Voorvoegsel = true,
                Geslachtsnaam = true,
                Voorletters = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "020210" or
            "030210" or
            "050210" or
            "090210" => new BrpApiDtos.NaamInOnderzoek
            {
                Voornamen = true,
                Voorletters = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "020220" or
            "030220" or
            "050220" or
            "090220" => new BrpApiDtos.NaamInOnderzoek
            {
                AdellijkeTitelPredicaat = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "020230" or
            "030230" or
            "050230" or
            "090230" => new BrpApiDtos.NaamInOnderzoek
            {
                Voorvoegsel = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "020240" or
            "030240" or
            "050240" or
            "090240" => new BrpApiDtos.NaamInOnderzoek
            {
                Geslachtsnaam = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null
        };
    }
}
