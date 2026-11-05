using Brp.Shared.DtoMappers.BrpApiDtos;
using System.Collections.ObjectModel;

namespace Brp.Shared.DtoMappers.Mappers;

public static class PartnerMapper
{
    public static Collection<Partner>? Map(this ICollection<BrpDtos.GbaPartner> partners)
    {
        var retval = new Collection<Partner>();
        foreach (var partner in from partner in partners
                                where partner != null
                                select partner)
        {
            retval.Add(partner.Map()!);
        }
        return retval;
    }

    public static BrpApiDtos.Partner? Map(this BrpDtos.GbaPartner? partner)
    {
        return partner == null
            ? null
            : new BrpApiDtos.Partner
            {
                Naam = partner.Naam.MapNaamGerelateerde(partner.InOnderzoek),
                SoortVerbintenis = partner.SoortVerbintenis.MapSoortVerbintenis(),
                Geboorte = partner.Geboorte.Map(partner.InOnderzoek),
                Burgerservicenummer = partner.Burgerservicenummer,
                Geslacht = partner.Geslacht?.Map(),
                AangaanHuwelijkPartnerschap = partner.AangaanHuwelijkPartnerschap.Map(partner.InOnderzoek),
                OntbindingHuwelijkPartnerschap = partner.OntbindingHuwelijkPartnerschap.Map(partner.InOnderzoek),
                InOnderzoek = partner.InOnderzoek.PartnerInOnderzoek()
            };
    }

    public static BrpApiDtos.OntbindingHuwelijkPartnerschap? Map(this BrpDtos.GbaOntbindingHuwelijkPartnerschap? ontbindingHuwelijkPartnerschap, BrpDtos.InOnderzoek inOnderzoek)
    {
        if (ontbindingHuwelijkPartnerschap == null && inOnderzoek != null)
        {
          ontbindingHuwelijkPartnerschap = new BrpDtos.GbaOntbindingHuwelijkPartnerschap { InOnderzoek = inOnderzoek };
        }
        return ontbindingHuwelijkPartnerschap == null
            ? null
            : new BrpApiDtos.OntbindingHuwelijkPartnerschap
            {
              Datum = ontbindingHuwelijkPartnerschap.Datum?.Map(),
              InOnderzoek = ontbindingHuwelijkPartnerschap.InOnderzoek.OntbondenPartnerInOnderzoek()
            };
    }

    public static BrpApiDtos.AangaanHuwelijkPartnerschap? Map(this BrpDtos.GbaAangaanHuwelijkPartnerschap? aangaanHuwelijkPartnerschap, BrpDtos.InOnderzoek inOnderzoek)
    {
        if (aangaanHuwelijkPartnerschap == null && inOnderzoek != null)
        {
            aangaanHuwelijkPartnerschap = new BrpDtos.GbaAangaanHuwelijkPartnerschap { InOnderzoek = inOnderzoek };
        }
        return aangaanHuwelijkPartnerschap == null
            ? null
            : new BrpApiDtos.AangaanHuwelijkPartnerschap
            {
                Datum = aangaanHuwelijkPartnerschap.Datum?.Map(),
                Land = aangaanHuwelijkPartnerschap.Land.MapLand(),
                Plaats = aangaanHuwelijkPartnerschap.Plaats.MapPlaats(),
                InOnderzoek = aangaanHuwelijkPartnerschap.InOnderzoek.AangaanHuwelijkPartnerschapInOnderzoek()
            };
    }

    public static BrpApiDtos.AangaanHuwelijkPartnerschapInOnderzoek? AangaanHuwelijkPartnerschapInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            "050000" or
            "050600" => new BrpApiDtos.AangaanHuwelijkPartnerschapInOnderzoek
            {
                Datum = true,
                Land = true,
                Plaats = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "050610" => new BrpApiDtos.AangaanHuwelijkPartnerschapInOnderzoek
            {
                Datum = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "050620" => new BrpApiDtos.AangaanHuwelijkPartnerschapInOnderzoek
            {
                Plaats = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "050630" => new BrpApiDtos.AangaanHuwelijkPartnerschapInOnderzoek
            {
                Land = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }

    public static BrpApiDtos.PartnerInOnderzoek? PartnerInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            "050000" => new BrpApiDtos.PartnerInOnderzoek
            {
                Burgerservicenummer = true,
                SoortVerbintenis = true,
                Geslacht = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "050100" or "050120" => new BrpApiDtos.PartnerInOnderzoek
            {
                Burgerservicenummer = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "050400" or "050410" => new BrpApiDtos.PartnerInOnderzoek
            {
                Geslacht = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "051500" or "051510" => new BrpApiDtos.PartnerInOnderzoek
            {
                SoortVerbintenis = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }

    public static BrpApiDtos.OntbindingHuwelijkPartnerschapInOnderzoek? OntbondenPartnerInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            "050000" or
            "050700" or
            "050710" => new BrpApiDtos.OntbindingHuwelijkPartnerschapInOnderzoek
            {
                Datum = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null
        };
    }

    private static CommonDtos.Waardetabel? MapSoortVerbintenis(this CommonDtos.Waardetabel? soortVerbintenis) =>
      soortVerbintenis?.Code == "."
        ? null
        : soortVerbintenis?.Map();
}
