namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class Geboorte : IDatumPlaatsLandDto
{
    public bool ShouldSerialize() =>
        Datum != null ||
        Land != null ||
        Plaats != null ||
        InOnderzoek != null
        ;

    public bool ShouldSerializeInOnderzoek() => InOnderzoek != null && InOnderzoek.ShouldSerialize();
}

public partial class GeboorteInOnderzoek
{
    public bool ShouldSerialize() =>
        Datum.HasValue ||
        Land.HasValue ||
        Plaats.HasValue;
}