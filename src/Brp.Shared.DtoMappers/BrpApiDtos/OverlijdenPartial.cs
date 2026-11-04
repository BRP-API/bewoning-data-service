namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class Overlijden : IDatumPlaatsLandDto
{
    public bool ShouldSerialize() =>
        Datum != null ||
        Land != null ||
        Plaats != null ||
        InOnderzoek != null
        ;

    public bool ShouldSerializeInOnderzoek() => InOnderzoek != null && InOnderzoek.ShouldSerialize();
}

public partial class OverlijdenInOnderzoek
{
    public bool ShouldSerialize() =>
        Datum.HasValue ||
        Plaats.HasValue ||
        Land.HasValue;
}
