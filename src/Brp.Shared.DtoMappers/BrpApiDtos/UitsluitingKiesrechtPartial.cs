namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class UitsluitingKiesrecht
{
    public bool ShouldSerialize() =>
        UitgeslotenVanKiesrecht == true ||
        Einddatum != null;
}
