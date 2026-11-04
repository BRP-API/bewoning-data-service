namespace Brp.Shared.DtoMappers.Mappers;

public static class RniMapper
{
    public static CommonDtos.RniDeelnemer? Map(this CommonDtos.RniDeelnemer? rniDeelnemer)
    {
        return rniDeelnemer == null
            ? null
            : new CommonDtos.RniDeelnemer
            {
                Categorie = rniDeelnemer.Categorie,
                OmschrijvingVerdrag = rniDeelnemer.OmschrijvingVerdrag,
                Deelnemer = rniDeelnemer.Deelnemer.Map(),
            };
    }

    public static ICollection<CommonDtos.RniDeelnemer>? Map(this ICollection<CommonDtos.RniDeelnemer>? rniDeelnemers)
    {
        return rniDeelnemers?.Select(x => x.Map()).ToList();
    }
}
