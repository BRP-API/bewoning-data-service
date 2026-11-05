namespace Brp.Shared.DtoMappers.Mappers;

public static class AdellijkeTitelPredicaatTypeMapper
{
    public static CommonDtos.AdellijkeTitelPredicaatType? Map(this CommonDtos.AdellijkeTitelPredicaatType? adellijkeTitelPredicaatType)
    {
        return adellijkeTitelPredicaatType == null
            ? null
            : new CommonDtos.AdellijkeTitelPredicaatType
            {
                Soort = adellijkeTitelPredicaatType.Soort,
                Code = adellijkeTitelPredicaatType.Code,
                Omschrijving = adellijkeTitelPredicaatType.Omschrijving
            };
    }
}
