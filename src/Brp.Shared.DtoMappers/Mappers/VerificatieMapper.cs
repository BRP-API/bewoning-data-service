namespace Brp.Shared.DtoMappers.Mappers;

public static class VerificatieMapper
{
    public static BrpApiDtos.Verificatie? Map(this BrpDtos.GbaVerificatie? verificatie)
    {
        return verificatie == null
            ? null
            : new BrpApiDtos.Verificatie
            {
                Datum = verificatie.Datum.Map(),
                Omschrijving = verificatie.Omschrijving
            };
    }
}
