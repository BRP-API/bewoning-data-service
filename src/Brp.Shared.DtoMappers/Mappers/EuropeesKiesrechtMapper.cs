namespace Brp.Shared.DtoMappers.Mappers;

public static class EuropeesKiesrechtMapper
{
    public static BrpApiDtos.EuropeesKiesrecht? Map(this BrpDtos.GbaEuropeesKiesrecht? europeesKiesrecht)
    {
        return europeesKiesrecht == null
            ? null
            : new BrpApiDtos.EuropeesKiesrecht
            {
                Aanduiding = europeesKiesrecht.Aanduiding.Map(),
                EinddatumUitsluiting = europeesKiesrecht.EinddatumUitsluiting?.Map()
            };
    }
}
