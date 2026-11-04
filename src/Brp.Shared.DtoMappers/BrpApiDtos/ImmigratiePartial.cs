namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class Immigratie
{
    public bool ShouldSerialize() =>
        DatumVestigingInNederland != null ||
        ShouldSerializeIndicatieVestigingVanuitBuitenland() ||
        InOnderzoek != null ||
        LandVanwaarIngeschreven != null ||
        ShouldSerializeVanuitVerblijfplaatsOnbekend()
        ;

    public bool ShouldSerializeIndicatieVestigingVanuitBuitenland() => IndicatieVestigingVanuitBuitenland.HasValue && IndicatieVestigingVanuitBuitenland.Value;
    public bool ShouldSerializeVanuitVerblijfplaatsOnbekend() => VanuitVerblijfplaatsOnbekend.HasValue && VanuitVerblijfplaatsOnbekend.Value;
}
