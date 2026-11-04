namespace Brp.Shared.DtoMappers.Mappers;

public static class WaardetabelMapper
{
    public static Brp.Shared.DtoMappers.CommonDtos.Waardetabel? Map(this CommonDtos.Waardetabel? waardetabel)
    {
        return waardetabel == null
            ? null
            : new CommonDtos.Waardetabel
            {
                Code = waardetabel.Code,
                Omschrijving = waardetabel.Omschrijving
            };
    }
}
