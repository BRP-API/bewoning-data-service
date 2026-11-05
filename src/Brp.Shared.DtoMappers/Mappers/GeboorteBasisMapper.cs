using Brp.Shared.DtoMappers.BrpApiDtos;

namespace Brp.Shared.DtoMappers.Mappers;

public static class GeboorteBasisMapper
{
    public static GeboorteBeperkt? Map(this BrpDtos.GeboorteBasis? geboorteBasis, BrpDtos.InOnderzoek? inOnderzoek)
    {
        return geboorteBasis == null
            ? null
            : new GeboorteBeperkt
            {
                Datum = geboorteBasis.Datum?.Map(),
                InOnderzoek = inOnderzoek.MapGeboorteBeperktInOnderzoek()
            };
    }

    public static GeboorteInOnderzoekBeperkt? MapGeboorteBeperktInOnderzoek(this BrpDtos.InOnderzoek? source)
    {
        return source?.AanduidingGegevensInOnderzoek switch
        {
            "010000" or
            "010300" or
            "010310" => new BrpApiDtos.GeboorteInOnderzoekBeperkt
            {
                Datum = true,
                DatumIngangOnderzoek = source.DatumIngangOnderzoek.Map()
            },
            _ => null,
        };
    }
}
