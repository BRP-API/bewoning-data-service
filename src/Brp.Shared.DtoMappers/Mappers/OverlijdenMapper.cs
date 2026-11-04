using Brp.Shared.DtoMappers.BrpApiDtos;

namespace Brp.Shared.DtoMappers.Mappers;

public static class OverlijdenMapper
{
    public static Overlijden? Map(this BrpDtos.GbaOverlijden overlijden)
    {
        return overlijden == null
            ? null
            : new Overlijden
            {
                Datum = overlijden.Datum?.Map(),
                Plaats = overlijden.Plaats.MapPlaats(),
                Land = overlijden.Land.MapLand(),
                InOnderzoek = overlijden.InOnderzoek.OverlijdenInOnderzoek()
            };
    }

    private static BrpApiDtos.OverlijdenInOnderzoek? OverlijdenInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        if (source == null)
        {
            return null;
        }

        return source.AanduidingGegevensInOnderzoek switch
        {
            "060000" or
            "060800" => new BrpApiDtos.OverlijdenInOnderzoek
            {
                Datum = true,
                Land = true,
                Plaats = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "060810" => new BrpApiDtos.OverlijdenInOnderzoek
            {
                Datum = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "060820" => new BrpApiDtos.OverlijdenInOnderzoek
            {
                Plaats = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            "060830" => new BrpApiDtos.OverlijdenInOnderzoek
            {
                Land = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }
}
