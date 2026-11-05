using Brp.Shared.DtoMappers.Interfaces;

namespace Brp.Shared.DtoMappers.BrpDtos;

public partial class GbaNaamPersoon : INaam
{
    public ICollection<GbaPartner>? Partners { get; set; }
}
