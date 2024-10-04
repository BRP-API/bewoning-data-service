namespace Brp.AutorisatieEnProtocollering.Proxy.Validatie.Personen;

internal static class Constanten
{
    internal static readonly string[] FieldsBeperktAdressering = new[]
    {
        "adressering",
        "adressering.adresregel1",
        "adressering.adresregel2",
        "adressering.adresregel3",
        "adressering.land",
        "adressering.land.code",
        "adressering.land.omschrijving",

        "adressering.indicatieVastgesteldVerblijftNietOpAdres",

        "adressering.inOnderzoek",
        "adressering.inOnderzoek.adresregel1",
        "adressering.inOnderzoek.adresregel2",
        "adressering.inOnderzoek.adresregel3",
        "adressering.inOnderzoek.land",
        "adressering.inOnderzoek.datumIngangOnderzoekVerblijfplaats",
        "adressering.inOnderzoek.datumIngangOnderzoekVerblijfplaats.type",
        "adressering.inOnderzoek.datumIngangOnderzoekVerblijfplaats.datum",
        "adressering.inOnderzoek.datumIngangOnderzoekVerblijfplaats.langFormaat",
        "adressering.inOnderzoek.datumIngangOnderzoekVerblijfplaats.onbekend",
        "adressering.inOnderzoek.datumIngangOnderzoekVerblijfplaats.jaar",
        "adressering.inOnderzoek.datumIngangOnderzoekVerblijfplaats.maand",
    };

    internal static readonly string[] FieldsAdressering = 
        FieldsBeperktAdressering
        .Concat(new[]
        {
            "adressering.aanhef",
            "adressering.aanschrijfwijze",
            "adressering.aanschrijfwijze.naam",
            "adressering.aanschrijfwijze.aanspreekvorm",
            "adressering.gebruikInLopendeTekst",

            "adressering.inOnderzoek.aanhef",
            "adressering.inOnderzoek.aanschrijfwijze",
            "adressering.inOnderzoek.gebruikInLopendeTekst",

            "adressering.inOnderzoek.datumIngangOnderzoekPersoon",
            "adressering.inOnderzoek.datumIngangOnderzoekPersoon.type",
            "adressering.inOnderzoek.datumIngangOnderzoekPersoon.datum",
            "adressering.inOnderzoek.datumIngangOnderzoekPersoon.langFormaat",
            "adressering.inOnderzoek.datumIngangOnderzoekPersoon.onbekend",
            "adressering.inOnderzoek.datumIngangOnderzoekPersoon.jaar",
            "adressering.inOnderzoek.datumIngangOnderzoekPersoon.maand",

            "adressering.inOnderzoek.datumIngangOnderzoekPartner",
            "adressering.inOnderzoek.datumIngangOnderzoekPartner.type",
            "adressering.inOnderzoek.datumIngangOnderzoekPartner.datum",
            "adressering.inOnderzoek.datumIngangOnderzoekPartner.langFormaat",
            "adressering.inOnderzoek.datumIngangOnderzoekPartner.onbekend",
            "adressering.inOnderzoek.datumIngangOnderzoekPartner.jaar",
            "adressering.inOnderzoek.datumIngangOnderzoekPartner.maand",
        })
        .ToArray();

    internal static readonly string[] FieldsBeperktAdresseringBinnenland = new[]
    {
        "adresseringBinnenland",
        "adresseringBinnenland.adresregel1",
        "adresseringBinnenland.adresregel2",

        "adresseringBinnenland.indicatieVastgesteldVerblijftNietOpAdres",

        "adresseringBinnenland.inOnderzoek",
        "adresseringBinnenland.inOnderzoek.adresregel1",
        "adresseringBinnenland.inOnderzoek.adresregel2",
        "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekVerblijfplaats",
        "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekVerblijfplaats.type",
        "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekVerblijfplaats.datum",
        "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekVerblijfplaats.langFormaat",
        "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekVerblijfplaats.onbekend",
        "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekVerblijfplaats.jaar",
        "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekVerblijfplaats.maand",
    };

    internal static readonly string[] FieldsAdresseringBinnenland =
        FieldsBeperktAdresseringBinnenland
        .Concat(new[]
        {
            "adresseringBinnenland.aanhef",
            "adresseringBinnenland.aanschrijfwijze",
            "adresseringBinnenland.aanschrijfwijze.naam",
            "adresseringBinnenland.aanschrijfwijze.aanspreekvorm",
            "adresseringBinnenland.gebruikInLopendeTekst",

            "adresseringBinnenland.inOnderzoek.aanhef",
            "adresseringBinnenland.inOnderzoek.aanschrijfwijze",
            "adresseringBinnenland.inOnderzoek.gebruikInLopendeTekst",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPersoon",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPersoon.type",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPersoon.datum",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPersoon.langFormaat",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPersoon.onbekend",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPersoon.jaar",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPersoon.maand",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPartner",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPartner.type",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPartner.datum",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPartner.langFormaat",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPartner.onbekend",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPartner.jaar",
            "adresseringBinnenland.inOnderzoek.datumIngangOnderzoekPartner.maand",
        })
        .ToArray();

    internal static readonly string[] FieldsDatumEersteInschrijvingGba = new[]
    {
        "datumEersteInschrijvingGBA",
        "datumEersteInschrijvingGBA.type",
        "datumEersteInschrijvingGBA.datum",
        "datumEersteInschrijvingGBA.langFormaat",
        "datumEersteInschrijvingGBA.onbekend",
        "datumEersteInschrijvingGBA.jaar",
        "datumEersteInschrijvingGBA.maand",
    };

    internal static readonly string[] FieldsDatumInschrijvingInGemeente = new[]
    {
        "datumInschrijvingInGemeente",
        "datumInschrijvingInGemeente.type",
        "datumInschrijvingInGemeente.datum",
        "datumInschrijvingInGemeente.langFormaat",
        "datumInschrijvingInGemeente.onbekend",
        "datumInschrijvingInGemeente.jaar",
        "datumInschrijvingInGemeente.maand",

        "inOnderzoek.datumInschrijvingInGemeente",
    };

    internal static readonly string[] FieldsEuropeesKiesrecht = new[]
    {
        "europeesKiesrecht",
        "europeesKiesrecht.aanduiding",
        "europeesKiesrecht.aanduiding.code",
        "europeesKiesrecht.aanduiding.omschrijving",
        "europeesKiesrecht.einddatumUitsluiting",
        "europeesKiesrecht.einddatumUitsluiting.type",
        "europeesKiesrecht.einddatumUitsluiting.datum",
        "europeesKiesrecht.einddatumUitsluiting.langFormaat",
        "europeesKiesrecht.einddatumUitsluiting.onbekend",
        "europeesKiesrecht.einddatumUitsluiting.jaar",
        "europeesKiesrecht.einddatumUitsluiting.maand",
    };

    internal static readonly string[] FieldsBeperktGeboorte = new[]
    {
        "geboorte",
        "geboorte.datum",
        "geboorte.datum.type",
        "geboorte.datum.datum",
        "geboorte.datum.langFormaat",
        "geboorte.datum.onbekend",
        "geboorte.datum.jaar",
        "geboorte.datum.maand",

        "geboorte.inOnderzoek",
        "geboorte.inOnderzoek.datum",
        "geboorte.inOnderzoek.datumIngangOnderzoek",
        "geboorte.inOnderzoek.datumIngangOnderzoek.type",
        "geboorte.inOnderzoek.datumIngangOnderzoek.datum",
        "geboorte.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "geboorte.inOnderzoek.datumIngangOnderzoek.onbekend",
        "geboorte.inOnderzoek.datumIngangOnderzoek.jaar",
        "geboorte.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] FieldsGeboorte =
        FieldsBeperktGeboorte
        .Concat(new[]
        {
            "geboorte.land",
            "geboorte.land.code",
            "geboorte.land.omschrijving",
            "geboorte.plaats",
            "geboorte.plaats.code",
            "geboorte.plaats.omschrijving",

            "geboorte.inOnderzoek.land",
            "geboorte.inOnderzoek.plaats",
        })
        .ToArray();

    internal static readonly string[] FieldsGeheimhouding = new[]
    {
        "geheimhoudingPersoonsgegevens",
    };

    internal static readonly string[] FieldsGemeenteVanInschrijving = new[]
    {
        "gemeenteVanInschrijving",
        "gemeenteVanInschrijving.code",
        "gemeenteVanInschrijving.omschrijving",

        "inOnderzoek.gemeenteVanInschrijving",
    };

    internal static readonly string[] FieldsGeslacht = new[]
    {
        "geslacht",
        "geslacht.code",
        "geslacht.omschrijving",

        "inOnderzoek.geslacht",
    };

    internal static readonly string[] FieldsGezag = new[]
    {
        "gezag",
        "gezag.type",
        "gezag.minderjarige",
        "gezag.ouders",
        "gezag.ouder",
        "gezag.derde",
        "gezag.derden",
        "gezag.minderjarige.burgerservicenummer",
        "gezag.ouders.burgerservicenummer",
        "gezag.ouder.burgerservicenummer",
        "gezag.derde.burgerservicenummer",
        "gezag.derden.burgerservicenummer",
    };

    internal static readonly string[] FieldsBeperktIdentiteit = new[]
    {
        "burgerservicenummer",

        "inOnderzoek.burgerservicenummer",
    };

    internal static readonly string[] FieldsIdentiteit =
        FieldsBeperktIdentiteit
        .Concat(new[]
        {
            "aNummer",
        })
        .ToArray();

    internal static readonly string[] FieldsImmigratie = new[]
    {
        "immigratie",
        "immigratie.datumVestigingInNederland",
        "immigratie.datumVestigingInNederland.type",
        "immigratie.datumVestigingInNederland.datum",
        "immigratie.datumVestigingInNederland.langFormaat",
        "immigratie.datumVestigingInNederland.onbekend",
        "immigratie.datumVestigingInNederland.jaar",
        "immigratie.datumVestigingInNederland.maand",
        "immigratie.indicatieVestigingVanuitBuitenland",
        "immigratie.landVanwaarIngeschreven",
        "immigratie.landVanwaarIngeschreven.code",
        "immigratie.landVanwaarIngeschreven.omschrijving",
        "immigratie.vanuitVerblijfplaatsOnbekend",

        "immigratie.inOnderzoek",
        "immigratie.inOnderzoek.datumVestigingInNederland",
        "immigratie.inOnderzoek.landVanwaarIngeschreven",
        "immigratie.inOnderzoek.vanuitVerblijfplaatsOnbekend",
        "immigratie.inOnderzoek.indicatieVestigingVanuitBuitenland",
        "immigratie.inOnderzoek.datumIngangOnderzoek",
        "immigratie.inOnderzoek.datumIngangOnderzoek.type",
        "immigratie.inOnderzoek.datumIngangOnderzoek.datum",
        "immigratie.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "immigratie.inOnderzoek.datumIngangOnderzoek.onbekend",
        "immigratie.inOnderzoek.datumIngangOnderzoek.jaar",
        "immigratie.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] FieldsIndicatieCurateleRegister = new[]
    {
        "indicatieCurateleRegister",

        "inOnderzoek.indicatieCurateleRegister",
    };

    internal static readonly string[] FieldsIndicatieGezagMinderjarige = new[]
    {
        "indicatieGezagMinderjarige",
        "indicatieGezagMinderjarige.code",
        "indicatieGezagMinderjarige.omschrijving",

        "inOnderzoek.indicatieGezagMinderjarige",
    };

    internal static readonly string[] FieldsInOnderzoekGemeente = new[]
    {
        "inOnderzoek.datumIngangOnderzoekGemeente",
        "inOnderzoek.datumIngangOnderzoekGemeente.type",
        "inOnderzoek.datumIngangOnderzoekGemeente.datum",
        "inOnderzoek.datumIngangOnderzoekGemeente.langFormaat",
        "inOnderzoek.datumIngangOnderzoekGemeente.onbekend",
        "inOnderzoek.datumIngangOnderzoekGemeente.jaar",
        "inOnderzoek.datumIngangOnderzoekGemeente.maand",
    };

    internal static readonly string[] FieldsInOnderzoekGezag = new[]
    {
        "inOnderzoek.datumIngangOnderzoekGezag",
        "inOnderzoek.datumIngangOnderzoekGezag.type",
        "inOnderzoek.datumIngangOnderzoekGezag.datum",
        "inOnderzoek.datumIngangOnderzoekGezag.langFormaat",
        "inOnderzoek.datumIngangOnderzoekGezag.onbekend",
        "inOnderzoek.datumIngangOnderzoekGezag.jaar",
        "inOnderzoek.datumIngangOnderzoekGezag.maand",
    };

    internal static readonly string[] FieldsInOnderzoekPersoon = new[]
    {
        "inOnderzoek",
        "inOnderzoek.datumIngangOnderzoekPersoon",
        "inOnderzoek.datumIngangOnderzoekPersoon.type",
        "inOnderzoek.datumIngangOnderzoekPersoon.datum",
        "inOnderzoek.datumIngangOnderzoekPersoon.langFormaat",
        "inOnderzoek.datumIngangOnderzoekPersoon.onbekend",
        "inOnderzoek.datumIngangOnderzoekPersoon.jaar",
        "inOnderzoek.datumIngangOnderzoekPersoon.maand",
    };

    internal static readonly string[] FieldsKind = new[]
    {
        "kinderen",
        "kinderen.burgerservicenummer",

        "kinderen.inOnderzoek",
        "kinderen.inOnderzoek.burgerservicenummer",
        "kinderen.inOnderzoek.datumIngangOnderzoek",
        "kinderen.inOnderzoek.datumIngangOnderzoek.type",
        "kinderen.inOnderzoek.datumIngangOnderzoek.datum",
        "kinderen.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "kinderen.inOnderzoek.datumIngangOnderzoek.onbekend",
        "kinderen.inOnderzoek.datumIngangOnderzoek.jaar",
        "kinderen.inOnderzoek.datumIngangOnderzoek.maand",

        "kinderen.naam",
        "kinderen.naam.adellijkeTitelPredicaat",
        "kinderen.naam.adellijkeTitelPredicaat.soort",
        "kinderen.naam.adellijkeTitelPredicaat.code",
        "kinderen.naam.adellijkeTitelPredicaat.omschrijving",
        "kinderen.naam.geslachtsnaam",
        "kinderen.naam.voorletters",
        "kinderen.naam.voornamen",
        "kinderen.naam.voorvoegsel",

        "kinderen.naam.inOnderzoek",
        "kinderen.naam.inOnderzoek.adellijkeTitelPredicaat",
        "kinderen.naam.inOnderzoek.geslachtsnaam",
        "kinderen.naam.inOnderzoek.voorletters",
        "kinderen.naam.inOnderzoek.voornamen",
        "kinderen.naam.inOnderzoek.voorvoegsel",
        "kinderen.naam.inOnderzoek.datumIngangOnderzoek",
        "kinderen.naam.inOnderzoek.datumIngangOnderzoek.type",
        "kinderen.naam.inOnderzoek.datumIngangOnderzoek.datum",
        "kinderen.naam.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "kinderen.naam.inOnderzoek.datumIngangOnderzoek.onbekend",
        "kinderen.naam.inOnderzoek.datumIngangOnderzoek.jaar",
        "kinderen.naam.inOnderzoek.datumIngangOnderzoek.maand",

        "kinderen.geboorte",
        "kinderen.geboorte.datum",
        "kinderen.geboorte.datum.type",
        "kinderen.geboorte.datum.datum",
        "kinderen.geboorte.datum.langFormaat",
        "kinderen.geboorte.datum.onbekend",
        "kinderen.geboorte.datum.jaar",
        "kinderen.geboorte.datum.maand",
        "kinderen.geboorte.land",
        "kinderen.geboorte.land.code",
        "kinderen.geboorte.land.omschrijving",
        "kinderen.geboorte.plaats",
        "kinderen.geboorte.plaats.code",
        "kinderen.geboorte.plaats.omschrijving",

        "kinderen.geboorte.inOnderzoek",
        "kinderen.geboorte.inOnderzoek.datum",
        "kinderen.geboorte.inOnderzoek.land",
        "kinderen.geboorte.inOnderzoek.plaats",
        "kinderen.geboorte.inOnderzoek.datumIngangOnderzoek",
        "kinderen.geboorte.inOnderzoek.datumIngangOnderzoek.type",
        "kinderen.geboorte.inOnderzoek.datumIngangOnderzoek.datum",
        "kinderen.geboorte.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "kinderen.geboorte.inOnderzoek.datumIngangOnderzoek.onbekend",
        "kinderen.geboorte.inOnderzoek.datumIngangOnderzoek.jaar",
        "kinderen.geboorte.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] FieldsLeeftijd = new[]
    {
        "leeftijd",

        "inOnderzoek.leeftijd",
    };

    internal static readonly string[] FieldsBeperktNaam = new[]
    {
        "naam",

        "naam.adellijkeTitelPredicaat",
        "naam.adellijkeTitelPredicaat.soort",
        "naam.adellijkeTitelPredicaat.code",
        "naam.adellijkeTitelPredicaat.omschrijving",

        "naam.geslachtsnaam",

        "naam.volledigeNaam",

        "naam.voorletters",
        "naam.voornamen",
        "naam.voorvoegsel",

        "naam.inOnderzoek",
        "naam.inOnderzoek.datumIngangOnderzoek",
        "naam.inOnderzoek.datumIngangOnderzoek.type",
        "naam.inOnderzoek.datumIngangOnderzoek.datum",
        "naam.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "naam.inOnderzoek.datumIngangOnderzoek.onbekend",
        "naam.inOnderzoek.datumIngangOnderzoek.jaar",
        "naam.inOnderzoek.datumIngangOnderzoek.maand",

        "naam.inOnderzoek.adellijkeTitelPredicaat",
        "naam.inOnderzoek.geslachtsnaam",
        "naam.inOnderzoek.volledigeNaam",
        "naam.inOnderzoek.voorletters",
        "naam.inOnderzoek.voornamen",
        "naam.inOnderzoek.voorvoegsel",
    };

    internal static readonly string[] FieldsNaam =
        FieldsBeperktNaam
        .Concat(new[]
        {
            "naam.aanduidingNaamgebruik",
            "naam.aanduidingNaamgebruik.code",
            "naam.aanduidingNaamgebruik.omschrijving",

            "naam.inOnderzoek.aanduidingNaamgebruik",
        })
        .ToArray();

    internal static readonly string[] FieldsNationaliteit = new[]
    {
        "nationaliteiten",
        "nationaliteiten.type",
        "nationaliteiten.datumIngangGeldigheid",
        "nationaliteiten.datumIngangGeldigheid.type",
        "nationaliteiten.datumIngangGeldigheid.datum",
        "nationaliteiten.datumIngangGeldigheid.langFormaat",
        "nationaliteiten.datumIngangGeldigheid.onbekend",
        "nationaliteiten.datumIngangGeldigheid.jaar",
        "nationaliteiten.datumIngangGeldigheid.maand",
        "nationaliteiten.nationaliteit",
        "nationaliteiten.nationaliteit.code",
        "nationaliteiten.nationaliteit.omschrijving",
        "nationaliteiten.redenOpname",
        "nationaliteiten.redenOpname.code",
        "nationaliteiten.redenOpname.omschrijving",

        "nationaliteiten.inOnderzoek",
        "nationaliteiten.inOnderzoek.type",
        "nationaliteiten.inOnderzoek.nationaliteit",
        "nationaliteiten.inOnderzoek.redenOpname",
        "nationaliteiten.inOnderzoek.datumIngangOnderzoek",
        "nationaliteiten.inOnderzoek.datumIngangOnderzoek.type",
        "nationaliteiten.inOnderzoek.datumIngangOnderzoek.datum",
        "nationaliteiten.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "nationaliteiten.inOnderzoek.datumIngangOnderzoek.onbekend",
        "nationaliteiten.inOnderzoek.datumIngangOnderzoek.jaar",
        "nationaliteiten.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] FieldsOpschortingBijhouding = new[]
    {
        "opschortingBijhouding",
        "opschortingBijhouding.datum",
        "opschortingBijhouding.datum.type",
        "opschortingBijhouding.datum.datum",
        "opschortingBijhouding.datum.langFormaat",
        "opschortingBijhouding.datum.onbekend",
        "opschortingBijhouding.datum.jaar",
        "opschortingBijhouding.datum.maand",
        "opschortingBijhouding.reden",
        "opschortingBijhouding.reden.code",
        "opschortingBijhouding.reden.omschrijving",
    };

    internal static readonly string[] FieldsPartner = new[]
    {
        "partners",
        "partners.burgerservicenummer",

        "partners.geslacht",
        "partners.geslacht.code",
        "partners.geslacht.omschrijving",

        "partners.soortVerbintenis",
        "partners.soortVerbintenis.code",
        "partners.soortVerbintenis.omschrijving",

        "partners.inOnderzoek",
        "partners.inOnderzoek.burgerservicenummer",
        "partners.inOnderzoek.geslacht",
        "partners.inOnderzoek.soortVerbintenis",
        "partners.inOnderzoek.datumIngangOnderzoek",
        "partners.inOnderzoek.datumIngangOnderzoek.type",
        "partners.inOnderzoek.datumIngangOnderzoek.datum",
        "partners.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "partners.inOnderzoek.datumIngangOnderzoek.onbekend",
        "partners.inOnderzoek.datumIngangOnderzoek.jaar",
        "partners.inOnderzoek.datumIngangOnderzoek.maand",

        "partners.aangaanHuwelijkPartnerschap",
        "partners.aangaanHuwelijkPartnerschap.datum",
        "partners.aangaanHuwelijkPartnerschap.datum.type",
        "partners.aangaanHuwelijkPartnerschap.datum.datum",
        "partners.aangaanHuwelijkPartnerschap.datum.langFormaat",
        "partners.aangaanHuwelijkPartnerschap.datum.onbekend",
        "partners.aangaanHuwelijkPartnerschap.datum.jaar",
        "partners.aangaanHuwelijkPartnerschap.datum.maand",
        "partners.aangaanHuwelijkPartnerschap.land",
        "partners.aangaanHuwelijkPartnerschap.land.code",
        "partners.aangaanHuwelijkPartnerschap.land.omschrijving",
        "partners.aangaanHuwelijkPartnerschap.plaats",
        "partners.aangaanHuwelijkPartnerschap.plaats.code",
        "partners.aangaanHuwelijkPartnerschap.plaats.omschrijving",

        "partners.aangaanHuwelijkPartnerschap.inOnderzoek",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.datum",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.land",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.plaats",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.type",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.datum",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.onbekend",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.jaar",
        "partners.aangaanHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.maand",

        "partners.geboorte",
        "partners.geboorte.datum",
        "partners.geboorte.datum.type",
        "partners.geboorte.datum.datum",
        "partners.geboorte.datum.langFormaat",
        "partners.geboorte.datum.onbekend",
        "partners.geboorte.datum.jaar",
        "partners.geboorte.datum.maand",
        "partners.geboorte.land",
        "partners.geboorte.land.code",
        "partners.geboorte.land.omschrijving",
        "partners.geboorte.plaats",
        "partners.geboorte.plaats.code",
        "partners.geboorte.plaats.omschrijving",

        "partners.geboorte.inOnderzoek",
        "partners.geboorte.inOnderzoek.datum",
        "partners.geboorte.inOnderzoek.land",
        "partners.geboorte.inOnderzoek.plaats",
        "partners.geboorte.inOnderzoek.datumIngangOnderzoek",
        "partners.geboorte.inOnderzoek.datumIngangOnderzoek.type",
        "partners.geboorte.inOnderzoek.datumIngangOnderzoek.datum",
        "partners.geboorte.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "partners.geboorte.inOnderzoek.datumIngangOnderzoek.onbekend",
        "partners.geboorte.inOnderzoek.datumIngangOnderzoek.jaar",
        "partners.geboorte.inOnderzoek.datumIngangOnderzoek.maand",

        "partners.naam",
        "partners.naam.adellijkeTitelPredicaat",
        "partners.naam.adellijkeTitelPredicaat.soort",
        "partners.naam.adellijkeTitelPredicaat.code",
        "partners.naam.adellijkeTitelPredicaat.omschrijving",
        "partners.naam.geslachtsnaam",
        "partners.naam.voorletters",
        "partners.naam.voornamen",
        "partners.naam.voorvoegsel",

        "partners.naam.inOnderzoek",
        "partners.naam.inOnderzoek.adellijkeTitelPredicaat",
        "partners.naam.inOnderzoek.geslachtsnaam",
        "partners.naam.inOnderzoek.voorletters",
        "partners.naam.inOnderzoek.voornamen",
        "partners.naam.inOnderzoek.voorvoegsel",
        "partners.naam.inOnderzoek.datumIngangOnderzoek",
        "partners.naam.inOnderzoek.datumIngangOnderzoek.type",
        "partners.naam.inOnderzoek.datumIngangOnderzoek.datum",
        "partners.naam.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "partners.naam.inOnderzoek.datumIngangOnderzoek.onbekend",
        "partners.naam.inOnderzoek.datumIngangOnderzoek.jaar",
        "partners.naam.inOnderzoek.datumIngangOnderzoek.maand",

        "partners.ontbindingHuwelijkPartnerschap",
        "partners.ontbindingHuwelijkPartnerschap.datum",
        "partners.ontbindingHuwelijkPartnerschap.datum.type",
        "partners.ontbindingHuwelijkPartnerschap.datum.datum",
        "partners.ontbindingHuwelijkPartnerschap.datum.langFormaat",
        "partners.ontbindingHuwelijkPartnerschap.datum.onbekend",
        "partners.ontbindingHuwelijkPartnerschap.datum.jaar",
        "partners.ontbindingHuwelijkPartnerschap.datum.maand",

        "partners.ontbindingHuwelijkPartnerschap.inOnderzoek",
        "partners.ontbindingHuwelijkPartnerschap.inOnderzoek.datum",
        "partners.ontbindingHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek",
        "partners.ontbindingHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.type",
        "partners.ontbindingHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.datum",
        "partners.ontbindingHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "partners.ontbindingHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.onbekend",
        "partners.ontbindingHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.jaar",
        "partners.ontbindingHuwelijkPartnerschap.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] FieldsOuder = new[]
    {
        "ouders",
        "ouders.burgerservicenummer",

        "ouders.datumIngangFamilierechtelijkeBetrekking",
        "ouders.datumIngangFamilierechtelijkeBetrekking.type",
        "ouders.datumIngangFamilierechtelijkeBetrekking.datum",
        "ouders.datumIngangFamilierechtelijkeBetrekking.langFormaat",
        "ouders.datumIngangFamilierechtelijkeBetrekking.onbekend",
        "ouders.datumIngangFamilierechtelijkeBetrekking.jaar",
        "ouders.datumIngangFamilierechtelijkeBetrekking.maand",

        "ouders.geslacht",
        "ouders.geslacht.code",
        "ouders.geslacht.omschrijving",

        "ouders.ouderAanduiding",

        "ouders.inOnderzoek",
        "ouders.inOnderzoek.burgerservicenummer",
        "ouders.inOnderzoek.datumIngangFamilierechtelijkeBetrekking",
        "ouders.inOnderzoek.geslacht",
        "ouders.inOnderzoek.datumIngangOnderzoek",
        "ouders.inOnderzoek.datumIngangOnderzoek.type",
        "ouders.inOnderzoek.datumIngangOnderzoek.datum",
        "ouders.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "ouders.inOnderzoek.datumIngangOnderzoek.onbekend",
        "ouders.inOnderzoek.datumIngangOnderzoek.jaar",
        "ouders.inOnderzoek.datumIngangOnderzoek.maand",

        "ouders.geboorte",
        "ouders.geboorte.datum",
        "ouders.geboorte.datum.type",
        "ouders.geboorte.datum.datum",
        "ouders.geboorte.datum.langFormaat",
        "ouders.geboorte.datum.onbekend",
        "ouders.geboorte.datum.jaar",
        "ouders.geboorte.datum.maand",
        "ouders.geboorte.land",
        "ouders.geboorte.land.code",
        "ouders.geboorte.land.omschrijving",
        "ouders.geboorte.plaats",
        "ouders.geboorte.plaats.code",
        "ouders.geboorte.plaats.omschrijving",

        "ouders.geboorte.inOnderzoek",
        "ouders.geboorte.inOnderzoek.datum",
        "ouders.geboorte.inOnderzoek.land",
        "ouders.geboorte.inOnderzoek.plaats",
        "ouders.geboorte.inOnderzoek.datumIngangOnderzoek",
        "ouders.geboorte.inOnderzoek.datumIngangOnderzoek.type",
        "ouders.geboorte.inOnderzoek.datumIngangOnderzoek.datum",
        "ouders.geboorte.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "ouders.geboorte.inOnderzoek.datumIngangOnderzoek.onbekend",
        "ouders.geboorte.inOnderzoek.datumIngangOnderzoek.jaar",
        "ouders.geboorte.inOnderzoek.datumIngangOnderzoek.maand",

        "ouders.naam",
        "ouders.naam.adellijkeTitelPredicaat",
        "ouders.naam.adellijkeTitelPredicaat.soort",
        "ouders.naam.adellijkeTitelPredicaat.code",
        "ouders.naam.adellijkeTitelPredicaat.omschrijving",
        "ouders.naam.geslachtsnaam",
        "ouders.naam.voorletters",
        "ouders.naam.voornamen",
        "ouders.naam.voorvoegsel",

        "ouders.naam.inOnderzoek",
        "ouders.naam.inOnderzoek.adellijkeTitelPredicaat",
        "ouders.naam.inOnderzoek.geslachtsnaam",
        "ouders.naam.inOnderzoek.voorletters",
        "ouders.naam.inOnderzoek.voornamen",
        "ouders.naam.inOnderzoek.voorvoegsel",

        "ouders.naam.inOnderzoek.datumIngangOnderzoek",
        "ouders.naam.inOnderzoek.datumIngangOnderzoek.type",
        "ouders.naam.inOnderzoek.datumIngangOnderzoek.datum",
        "ouders.naam.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "ouders.naam.inOnderzoek.datumIngangOnderzoek.onbekend",
        "ouders.naam.inOnderzoek.datumIngangOnderzoek.jaar",
        "ouders.naam.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] FieldsOverlijden = new[]
    {
        "overlijden",
        "overlijden.datum",
        "overlijden.datum.type",
        "overlijden.datum.datum",
        "overlijden.datum.langFormaat",
        "overlijden.datum.onbekend",
        "overlijden.datum.jaar",
        "overlijden.datum.maand",
        "overlijden.land",
        "overlijden.land.code",
        "overlijden.land.omschrijving",
        "overlijden.plaats",
        "overlijden.plaats.code",
        "overlijden.plaats.omschrijving",

        "overlijden.inOnderzoek",
        "overlijden.inOnderzoek.datum",
        "overlijden.inOnderzoek.land",
        "overlijden.inOnderzoek.plaats",
        "overlijden.inOnderzoek.datumIngangOnderzoek",
        "overlijden.inOnderzoek.datumIngangOnderzoek.type",
        "overlijden.inOnderzoek.datumIngangOnderzoek.datum",
        "overlijden.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "overlijden.inOnderzoek.datumIngangOnderzoek.onbekend",
        "overlijden.inOnderzoek.datumIngangOnderzoek.jaar",
        "overlijden.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] FieldsRni = new[]
    {
        "rni",
        "rni.deelnemer",
        "rni.deelnemer.code",
        "rni.deelnemer.omschrijving",
        "rni.omschrijvingVerdrag",
        "rni.categorie",
    };

    internal static readonly string[] FieldsUitsluitingKiesrecht = new[]
    {
        "uitsluitingKiesrecht",
        "uitsluitingKiesrecht.uitgeslotenVanKiesrecht",
        "uitsluitingKiesrecht.einddatum",
        "uitsluitingKiesrecht.einddatum.type",
        "uitsluitingKiesrecht.einddatum.datum",
        "uitsluitingKiesrecht.einddatum.langFormaat",
        "uitsluitingKiesrecht.einddatum.onbekend",
        "uitsluitingKiesrecht.einddatum.jaar",
        "uitsluitingKiesrecht.einddatum.maand",
    };

    internal static readonly string[] FieldsVerblijfplaats = new[]
    {
        "verblijfplaats",
        "verblijfplaats.datumIngangGeldigheid",
        "verblijfplaats.datumIngangGeldigheid.type",
        "verblijfplaats.datumIngangGeldigheid.datum",
        "verblijfplaats.datumIngangGeldigheid.langFormaat",
        "verblijfplaats.datumIngangGeldigheid.onbekend",
        "verblijfplaats.datumIngangGeldigheid.jaar",
        "verblijfplaats.datumIngangGeldigheid.maand",
        "verblijfplaats.datumVan",
        "verblijfplaats.datumVan.type",
        "verblijfplaats.datumVan.datum",
        "verblijfplaats.datumVan.langFormaat",
        "verblijfplaats.datumVan.onbekend",
        "verblijfplaats.datumVan.jaar",
        "verblijfplaats.datumVan.maand",
        "verblijfplaats.type",

        "verblijfplaats.inOnderzoek",
        "verblijfplaats.inOnderzoek.datumIngangGeldigheid",
        "verblijfplaats.inOnderzoek.datumVan",
        "verblijfplaats.inOnderzoek.type",
        "verblijfplaats.inOnderzoek.datumIngangOnderzoek",
        "verblijfplaats.inOnderzoek.datumIngangOnderzoek.type",
        "verblijfplaats.inOnderzoek.datumIngangOnderzoek.datum",
        "verblijfplaats.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "verblijfplaats.inOnderzoek.datumIngangOnderzoek.onbekend",
        "verblijfplaats.inOnderzoek.datumIngangOnderzoek.jaar",
        "verblijfplaats.inOnderzoek.datumIngangOnderzoek.maand",

        "verblijfplaats.verblijfadres",
        "verblijfplaats.verblijfadres.regel1",
        "verblijfplaats.verblijfadres.regel2",
        "verblijfplaats.verblijfadres.regel3",
        "verblijfplaats.verblijfadres.land",
        "verblijfplaats.verblijfadres.land.code",
        "verblijfplaats.verblijfadres.land.omschrijving",

        "verblijfplaats.verblijfadres.inOnderzoek",
        "verblijfplaats.verblijfadres.inOnderzoek.regel1",
        "verblijfplaats.verblijfadres.inOnderzoek.regel2",
        "verblijfplaats.verblijfadres.inOnderzoek.regel3",
        "verblijfplaats.verblijfadres.inOnderzoek.land",
        "verblijfplaats.verblijfadres.inOnderzoek.datumIngangOnderzoek",
        "verblijfplaats.verblijfadres.inOnderzoek.datumIngangOnderzoek.type",
        "verblijfplaats.verblijfadres.inOnderzoek.datumIngangOnderzoek.datum",
        "verblijfplaats.verblijfadres.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "verblijfplaats.verblijfadres.inOnderzoek.datumIngangOnderzoek.onbekend",
        "verblijfplaats.verblijfadres.inOnderzoek.datumIngangOnderzoek.jaar",
        "verblijfplaats.verblijfadres.inOnderzoek.datumIngangOnderzoek.maand",

        "verblijfplaats.verblijfadres.aanduidingBijHuisnummer",
        "verblijfplaats.verblijfadres.aanduidingBijHuisnummer.code",
        "verblijfplaats.verblijfadres.aanduidingBijHuisnummer.omschrijving",
        "verblijfplaats.verblijfadres.huisletter",
        "verblijfplaats.verblijfadres.huisnummer",
        "verblijfplaats.verblijfadres.huisnummertoevoeging",
        "verblijfplaats.verblijfadres.korteStraatnaam",
        "verblijfplaats.verblijfadres.officieleStraatnaam",
        "verblijfplaats.verblijfadres.postcode",
        "verblijfplaats.verblijfadres.woonplaats",

        "verblijfplaats.verblijfadres.inOnderzoek.aanduidingBijHuisnummer",
        "verblijfplaats.verblijfadres.inOnderzoek.huisletter",
        "verblijfplaats.verblijfadres.inOnderzoek.huisnummer",
        "verblijfplaats.verblijfadres.inOnderzoek.huisnummertoevoeging",
        "verblijfplaats.verblijfadres.inOnderzoek.korteStraatnaam",
        "verblijfplaats.verblijfadres.inOnderzoek.officieleStraatnaam",
        "verblijfplaats.verblijfadres.inOnderzoek.postcode",
        "verblijfplaats.verblijfadres.inOnderzoek.woonplaats",

        "verblijfplaats.functieAdres",
        "verblijfplaats.functieAdres.code",
        "verblijfplaats.functieAdres.omschrijving",
        "verblijfplaats.adresseerbaarObjectIdentificatie",
        "verblijfplaats.nummeraanduidingIdentificatie",

        "verblijfplaats.inOnderzoek.functieAdres",
        "verblijfplaats.inOnderzoek.adresseerbaarObjectIdentificatie",
        "verblijfplaats.inOnderzoek.nummeraanduidingIdentificatie",

        "verblijfplaats.verblijfadres.locatiebeschrijving",
        "verblijfplaats.verblijfadres.inOnderzoek.locatiebeschrijving",

        "verblijfplaats.indicatieVastgesteldVerblijftNietOpAdres",
    };

    internal static readonly string[] FieldsVerblijfplaatsBinnenland = new[]
    {
        "verblijfplaatsBinnenland",

        "verblijfplaatsBinnenland.adresseerbaarObjectIdentificatie",
        "verblijfplaatsBinnenland.nummeraanduidingIdentificatie",

        "verblijfplaatsBinnenland.datumIngangGeldigheid",
        "verblijfplaatsBinnenland.datumIngangGeldigheid.type",
        "verblijfplaatsBinnenland.datumIngangGeldigheid.datum",
        "verblijfplaatsBinnenland.datumIngangGeldigheid.langFormaat",
        "verblijfplaatsBinnenland.datumIngangGeldigheid.jaar",
        "verblijfplaatsBinnenland.datumIngangGeldigheid.maand",
        "verblijfplaatsBinnenland.datumIngangGeldigheid.onbekend",
        "verblijfplaatsBinnenland.datumVan",
        "verblijfplaatsBinnenland.datumVan.type",
        "verblijfplaatsBinnenland.datumVan.datum",
        "verblijfplaatsBinnenland.datumVan.langFormaat",
        "verblijfplaatsBinnenland.datumVan.jaar",
        "verblijfplaatsBinnenland.datumVan.maand",
        "verblijfplaatsBinnenland.datumVan.onbekend",

        "verblijfplaatsBinnenland.functieAdres",
        "verblijfplaatsBinnenland.functieAdres.code",
        "verblijfplaatsBinnenland.functieAdres.omschrijving",

        "verblijfplaatsBinnenland.type",

        "verblijfplaatsBinnenland.verblijfadres",
        "verblijfplaatsBinnenland.verblijfadres.aanduidingBijHuisnummer",
        "verblijfplaatsBinnenland.verblijfadres.aanduidingBijHuisnummer.code",
        "verblijfplaatsBinnenland.verblijfadres.aanduidingBijHuisnummer.omschrijving",
        "verblijfplaatsBinnenland.verblijfadres.huisletter",
        "verblijfplaatsBinnenland.verblijfadres.huisnummer",
        "verblijfplaatsBinnenland.verblijfadres.huisnummertoevoeging",
        "verblijfplaatsBinnenland.verblijfadres.korteStraatnaam",
        "verblijfplaatsBinnenland.verblijfadres.officieleStraatnaam",
        "verblijfplaatsBinnenland.verblijfadres.postcode",
        "verblijfplaatsBinnenland.verblijfadres.woonplaats",

        "verblijfplaatsBinnenland.verblijfadres.locatiebeschrijving",
    };

    internal static readonly string[] FieldsVerblijfstitel = new[]
    {
        "verblijfstitel",
        "verblijfstitel.aanduiding",
        "verblijfstitel.aanduiding.code",
        "verblijfstitel.aanduiding.omschrijving",
        "verblijfstitel.datumEinde",
        "verblijfstitel.datumEinde.type",
        "verblijfstitel.datumEinde.datum",
        "verblijfstitel.datumEinde.langFormaat",
        "verblijfstitel.datumEinde.onbekend",
        "verblijfstitel.datumEinde.jaar",
        "verblijfstitel.datumEinde.maand",
        "verblijfstitel.datumIngang",
        "verblijfstitel.datumIngang.type",
        "verblijfstitel.datumIngang.datum",
        "verblijfstitel.datumIngang.langFormaat",
        "verblijfstitel.datumIngang.onbekend",
        "verblijfstitel.datumIngang.jaar",
        "verblijfstitel.datumIngang.maand",

        "verblijfstitel.inOnderzoek",
        "verblijfstitel.inOnderzoek.aanduiding",
        "verblijfstitel.inOnderzoek.datumEinde",
        "verblijfstitel.inOnderzoek.datumIngang",
        "verblijfstitel.inOnderzoek.datumIngangOnderzoek",
        "verblijfstitel.inOnderzoek.datumIngangOnderzoek.type",
        "verblijfstitel.inOnderzoek.datumIngangOnderzoek.datum",
        "verblijfstitel.inOnderzoek.datumIngangOnderzoek.langFormaat",
        "verblijfstitel.inOnderzoek.datumIngangOnderzoek.onbekend",
        "verblijfstitel.inOnderzoek.datumIngangOnderzoek.jaar",
        "verblijfstitel.inOnderzoek.datumIngangOnderzoek.maand",
    };

    internal static readonly string[] FieldsVerificatie = new[]
    {
        "verificatie",
        "verificatie.datum",
        "verificatie.datum.type",
        "verificatie.datum.datum",
        "verificatie.datum.langFormaat",
        "verificatie.datum.onbekend",
        "verificatie.datum.jaar",
        "verificatie.datum.maand",
        "verificatie.omschrijving",
    };

    internal static readonly string[] PersoonBeperktFields =
        FieldsInOnderzoekPersoon
        .Concat(FieldsBeperktAdressering)
        .Concat(FieldsBeperktAdresseringBinnenland)
        .Concat(FieldsBeperktGeboorte)
        .Concat(FieldsGeheimhouding)
        .Concat(FieldsGeslacht)
        .Concat(FieldsBeperktIdentiteit)
        .Concat(FieldsLeeftijd)
        .Concat(FieldsBeperktNaam)
        .Concat(FieldsOpschortingBijhouding)
        .Concat(FieldsRni)
        .Concat(FieldsVerificatie)
        .ToArray();

    internal static readonly string[] GezagPersoonBeperktFields =
        PersoonBeperktFields
        .Concat(FieldsGezag)
        .ToArray();

    internal static readonly string[] PersoonFields =
        FieldsInOnderzoekPersoon
        .Concat(FieldsInOnderzoekGemeente)
        .Concat(FieldsInOnderzoekGezag)
        .Concat(FieldsAdressering)
        .Concat(FieldsAdresseringBinnenland)
        .Concat(FieldsDatumEersteInschrijvingGba)
        .Concat(FieldsDatumInschrijvingInGemeente)
        .Concat(FieldsEuropeesKiesrecht)
        .Concat(FieldsGeboorte)
        .Concat(FieldsGeheimhouding)
        .Concat(FieldsGemeenteVanInschrijving)
        .Concat(FieldsGeslacht)
        .Concat(FieldsGezag)
        .Concat(FieldsIdentiteit)
        .Concat(FieldsImmigratie)
        .Concat(FieldsIndicatieCurateleRegister)
        .Concat(FieldsIndicatieGezagMinderjarige)
        .Concat(FieldsKind)
        .Concat(FieldsLeeftijd)
        .Concat(FieldsNaam)
        .Concat(FieldsNationaliteit)
        .Concat(FieldsOpschortingBijhouding)
        .Concat(FieldsOuder)
        .Concat(FieldsOverlijden)
        .Concat(FieldsPartner)
        .Concat(FieldsRni)
        .Concat(FieldsUitsluitingKiesrecht)
        .Concat(FieldsVerblijfplaats)
        .Concat(FieldsVerblijfplaatsBinnenland)
        .Concat(FieldsVerblijfstitel)
        .Concat(FieldsVerificatie)
        .ToArray();

    internal static readonly string[] NotAllowedPersoonFields = new[]
    {
        "opschortingBijhouding",
        "geheimhoudingPersoonsgegevens",
        "inOnderzoek",
        "verificatie",
        "rni",
        "indicatieVastgesteldVerblijftNietOpAdres"
    };
}
