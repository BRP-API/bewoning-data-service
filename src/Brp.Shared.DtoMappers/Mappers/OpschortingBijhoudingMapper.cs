using Brp.Shared.DtoMappers.BrpApiDtos;

namespace Brp.Shared.DtoMappers.Mappers;

public static class OpschortingBijhoudingMapper
{
    public static OpschortingBijhouding? Map(this BrpDtos.GbaOpschortingBijhouding opschortingBijhouding)
    {
        return opschortingBijhouding == null
            ? null
            : new OpschortingBijhouding
            {
                Datum = opschortingBijhouding.Datum?.Map(),
                Reden = opschortingBijhouding.Reden?.Map()
            };
    }
}
