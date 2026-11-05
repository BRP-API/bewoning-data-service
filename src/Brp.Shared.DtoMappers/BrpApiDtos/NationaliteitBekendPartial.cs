namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class NationaliteitBekend
{
    public bool ShouldSerializeInOnderzoek() => InOnderzoek != null && InOnderzoek.ShouldSerialize();
}

public partial class NationaliteitBekendInOnderzoek
{
    public bool ShouldSerialize() =>
        Nationaliteit.HasValue ||
        RedenOpname.HasValue ||
        Type.HasValue
        ;
}