namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class EuropeesKiesrecht
{
    public bool ShouldSerialize() =>
        Aanduiding != null ||
        EinddatumUitsluiting != null;
}
