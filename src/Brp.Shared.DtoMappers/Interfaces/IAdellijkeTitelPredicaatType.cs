using Brp.Shared.DtoMappers.CommonDtos;

namespace Brp.Shared.DtoMappers.Interfaces;

public interface IAdellijkeTitelPredicaatType : IWaardetabel
{
    AdellijkeTitelPredicaatSoort? Soort { get; }
}
