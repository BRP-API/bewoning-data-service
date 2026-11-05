using Brp.Shared.DtoMappers.BrpApiDtos;
using System.Collections.ObjectModel;

namespace Brp.Shared.DtoMappers.Mappers;

public static class OuderMapper
{
    public static Collection<Ouder>? Map(this ICollection<BrpDtos.GbaOuder> ouders)
    {
        var retval = new Collection<Ouder>();
        foreach (var ouder in from ouder in ouders
                              where ouder != null
                              select ouder)
        {
          retval.Add(ouder.Map()!);
        }

        return retval;
    }

    public static BrpApiDtos.Ouder? Map(this BrpDtos.GbaOuder? ouder)
    {
        return ouder == null
            ? null
            : new BrpApiDtos.Ouder
            {
                Burgerservicenummer = ouder.Burgerservicenummer,
                DatumIngangFamilierechtelijkeBetrekking = ouder.DatumIngangFamilierechtelijkeBetrekking?.Map(),
                Geboorte = ouder.Geboorte.Map(ouder.InOnderzoek),
                Geslacht = ouder.Geslacht.Map(),
                InOnderzoek = ouder.InOnderzoek.OuderInOnderzoek(),
                OuderAanduiding = ouder.OuderAanduiding,
                Naam = ouder.Naam.MapNaamGerelateerde(ouder.InOnderzoek),
            };
    }

    public static BrpApiDtos.OuderInOnderzoek? OuderInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            "020000" or
            "030000" => new BrpApiDtos.OuderInOnderzoek
            {
                Burgerservicenummer = true,
                Geslacht = true,
                DatumIngangFamilierechtelijkeBetrekking = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "020100" or
            "020120" or
            "030100" or
            "030120" => new BrpApiDtos.OuderInOnderzoek
            {
                Burgerservicenummer = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "020400" or
            "020410" or
            "030400" or
            "030410" => new BrpApiDtos.OuderInOnderzoek
            {
                Geslacht = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "026200" or
            "026210" or
            "036200" or
            "036210" => new BrpApiDtos.OuderInOnderzoek
            {
                DatumIngangFamilierechtelijkeBetrekking = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }
}
