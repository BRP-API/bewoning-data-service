using Brp.Shared.DtoMappers.CommonDtos;

namespace Brp.Shared.DtoMappers.Interfaces;

public interface INaamBasis
{
    AdellijkeTitelPredicaatType AdellijkeTitelPredicaat { get; }
    string Voornamen { get; }
    string Voorvoegsel { get; }
    string Geslachtsnaam { get; }
}
