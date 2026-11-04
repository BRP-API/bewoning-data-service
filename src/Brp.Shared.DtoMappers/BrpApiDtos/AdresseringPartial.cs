using Brp.Shared.DtoMappers.Interfaces;

namespace Brp.Shared.DtoMappers.BrpApiDtos;

public partial class Adressering : IAdressering
{
    public bool ShouldSerializeInOnderzoek() => InOnderzoek != null && InOnderzoek.ShouldSerialize();

    public bool ShouldSerialize() =>
        !string.IsNullOrWhiteSpace(Aanhef) ||
        Aanschrijfwijze != null ||
        !string.IsNullOrWhiteSpace(GebruikInLopendeTekst) ||
        !string.IsNullOrWhiteSpace(Adresregel1) ||
        !string.IsNullOrWhiteSpace(Adresregel2) ||
        !string.IsNullOrWhiteSpace(Adresregel3) ||
        Land != null ||
        InOnderzoek != null
        ;
}

public partial class AdresseringInOnderzoek : IAdresregelsInOnderzoek
{
    public bool ShouldSerialize() =>
        Aanhef.HasValue ||
        Aanschrijfwijze.HasValue ||
        GebruikInLopendeTekst.HasValue ||
        Adresregel1.HasValue ||
        Adresregel2.HasValue ||
        Adresregel3.HasValue ||
        Land.HasValue
        ;
}

public partial class AdresseringBeperkt : IAdressering
{
    public bool ShouldSerializeInOnderzoek() => InOnderzoek != null && InOnderzoek.ShouldSerialize();

    public bool ShouldSerialize() =>
        !string.IsNullOrWhiteSpace(Adresregel1) ||
        !string.IsNullOrWhiteSpace(Adresregel2) ||
        !string.IsNullOrWhiteSpace(Adresregel3) ||
        Land != null ||
        InOnderzoek != null
        ;
}

public partial class AdresseringInOnderzoekBeperkt : IAdresregelsInOnderzoek
{
    public bool ShouldSerialize() =>
        Adresregel1.HasValue ||
        Adresregel2.HasValue ||
        Adresregel3.HasValue ||
        Land.HasValue
        ;
}
