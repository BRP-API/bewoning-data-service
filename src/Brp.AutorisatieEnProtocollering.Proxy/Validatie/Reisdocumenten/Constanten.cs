namespace Brp.AutorisatieEnProtocollering.Proxy.Validatie.Reisdocumenten;

internal static class Constanten
{
    internal static readonly string[] FieldsReisdocument = new[]
    {
        "reisdocumentnummer",
        "soort",
        "soort.code",
        "soort.omschrijving",
        "datumEindeGeldigheid",
        "datumEindeGeldigheid.type",
        "datumEindeGeldigheid.datum",
        "datumEindeGeldigheid.langFormaat",
        "datumEindeGeldigheid.onbekend",
        "datumEindeGeldigheid.jaar",
        "datumEindeGeldigheid.maand",
    };

    internal static readonly string[] FieldsReisdocumentInOnderzoek = new[]
    {
        "inOnderzoek",
        "inOnderzoek.reisdocumentnummer",
        "inOnderzoek.soort",
        "inOnderzoek.datumEindeGeldigheid",
        "inOnderzoek.datumIngangOnderzoek",
        "inOnderzoek.datumIngangOnderzoek.type",
        "inOnderzoek.datumIngangOnderzoek.datum",
        "inOnderzoek.datumIngangOnderzoek.langFormaat",
        "inOnderzoek.datumIngangOnderzoek.onbekend",
        "inOnderzoek.datumIngangOnderzoek.jaar",
        "inOnderzoek.datumIngangOnderzoek.maand"
    };

    internal static readonly string[] FieldsInhoudingOfVermissing = new[]
    {
        "inhoudingOfVermissing",
        "inhoudingOfVermissing.datum",
        "inhoudingOfVermissing.datum.type",
        "inhoudingOfVermissing.datum.datum",
        "inhoudingOfVermissing.datum.langFormaat",
        "inhoudingOfVermissing.datum.onbekend",
        "inhoudingOfVermissing.datum.jaar",
        "inhoudingOfVermissing.datum.maand",
        "inhoudingOfVermissing.aanduiding",
        "inhoudingOfVermissing.aanduiding.code",
        "inhoudingOfVermissing.aanduiding.omschrijving",
    };

    internal static readonly string[] FieldsInhoudingOfVermissingInOnderzoek = new[]
    {
        "inhoudingOfVermissing.inOnderzoek",
        "inhoudingOfVermissing.inOnderzoek.datum",
        "inhoudingOfVermissing.inOnderzoek.aanduiding",
        "inhoudingOfVermissing.inOnderzoek.datumIngangOnderzoek",
        "inhoudingOfVermissing.inOnderzoek.datumIngangOnderzoek.type",
        "inhoudingOfVermissing.inOnderzoek.datumIngangOnderzoek.datum",
        "inhoudingOfVermissing.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "inhoudingOfVermissing.inOnderzoek.datumIngangOnderzoek.onbekend",
        "inhoudingOfVermissing.inOnderzoek.datumIngangOnderzoek.jaar",
        "inhoudingOfVermissing.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] FieldsHouder = new[]
    {
        "houder",
        "houder.burgerservicenummer",
        "houder.geheimhoudingPersoonsgegevens",
        "houder.opschortingBijhouding",
        "houder.opschortingBijhouding.datum",
        "houder.opschortingBijhouding.datum.type",
        "houder.opschortingBijhouding.datum.datum",
        "houder.opschortingBijhouding.datum.langFormaat",
        "houder.opschortingBijhouding.datum.onbekend",
        "houder.opschortingBijhouding.datum.jaar",
        "houder.opschortingBijhouding.datum.maand",
        "houder.opschortingBijhouding.reden",
        "houder.opschortingBijhouding.reden.code",
        "houder.opschortingBijhouding.reden.omschrijving",
    };

    internal static readonly string[] FieldsHouderInOnderzoek = new[]
    {
        "houder.inOnderzoek",
        "houder.inOnderzoek.burgerservicenummer",
        "houder.inOnderzoek.datumIngangOnderzoek",
        "houder.inOnderzoek.datumIngangOnderzoek.type",
        "houder.inOnderzoek.datumIngangOnderzoek.datum",
        "houder.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "houder.inOnderzoek.datumIngangOnderzoek.onbekend",
        "houder.inOnderzoek.datumIngangOnderzoek.jaar",
        "houder.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] ReisdocumentFields =
        FieldsReisdocument
        .Concat(FieldsReisdocumentInOnderzoek)
        .Concat(FieldsInhoudingOfVermissing)
        .Concat(FieldsInhoudingOfVermissingInOnderzoek)
        .Concat(FieldsHouder)
        .Concat(FieldsHouderInOnderzoek)
        .ToArray();

    internal static readonly string[] NotAllowedReisdocumentFields = new[]
    {
        "houder.geheimhoudingPersoonsgegevens",
        "houder.inOnderzoek",
        "houder.opschortingBijhouding",
        "inhoudingOfVermissing.inOnderzoek",
        "inOnderzoek"
    };
}
