using Brp.Shared.DtoMappers.Interfaces;

namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class NaamGerelateerde : INaamBasis
{
    public bool ShouldSerialize() =>
        AdellijkeTitelPredicaat != null ||
        !string.IsNullOrWhiteSpace(Geslachtsnaam) ||
        !string.IsNullOrWhiteSpace(Voornamen) ||
        !string.IsNullOrWhiteSpace(Voorvoegsel) ||
        !string.IsNullOrWhiteSpace(Voorletters) ||
        InOnderzoek != null
        ;

    public bool ShouldSerializeInOnderzoek() => InOnderzoek != null && InOnderzoek.ShouldSerialize();
}

public partial class NaamInOnderzoek
{
    public bool ShouldSerialize() =>
        AdellijkeTitelPredicaat.HasValue ||
        Geslachtsnaam.HasValue ||
        Voornamen.HasValue ||
        Voorvoegsel.HasValue ||
        Voorletters.HasValue
        ;
}