namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class OpschortingBijhouding
{
    public bool ShouldSerialize() =>
        Datum != null ||
        Reden != null;
}
