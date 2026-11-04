namespace Brp.Shared.DtoMappers.Mappers;

public static class UitsluitingKiesrechtMapper
{
    public static BrpApiDtos.UitsluitingKiesrecht? Map(this BrpDtos.GbaUitsluitingKiesrecht? uitsluitingKiesrecht)
    {
        return uitsluitingKiesrecht == null
            ? null
            : new BrpApiDtos.UitsluitingKiesrecht
            {
                UitgeslotenVanKiesrecht = uitsluitingKiesrecht.UitgeslotenVanKiesrecht,
                Einddatum = uitsluitingKiesrecht.Einddatum?.Map()
            };
    }
}
