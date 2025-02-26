using Rvig.BrpApi.Bewoningen.ApiModels.Bewoning;
using Rvig.BrpApi.Bewoningen.ApiModels.PersonenHistorieBase;
using Rvig.BrpApi.Bewoningen.ApiModels.Universal;

namespace Bewoning.Data.Service.Tests.Util;

public static class GbaBewonerFixture
{
    public static GbaBewoner CreateDefault()
    {
        return new GbaBewoner
        {
            Burgerservicenummer = "123456789",
            Geboorte = new GbaGeboorteBeperkt
            {
                Datum = "19900101"
            },
            Naam = new GbaNaamBasis
            {
                AdellijkeTitelPredicaat = null,
                Voornamen = "Test",
                Geslachtsnaam = "Aaa",
                Voorvoegsel = "van den"
            },
            GeheimhoudingPersoonsgegevens = 0,
            VerblijfplaatsInOnderzoek = new GbaInOnderzoek(),
            Geslacht = new Waardetabel
            {
                Code = "M",
                Omschrijving = "man"
            }
        };
    }
    public static GbaBewoner CreateDefaultMetGeboorte(string geboorteDatum)
    {
        return new GbaBewoner
        {
            Burgerservicenummer = "123456789",
            Geboorte = new GbaGeboorteBeperkt
            {
                Datum = geboorteDatum
            },
            Naam = new GbaNaamBasis
            {
                AdellijkeTitelPredicaat = null,
                Voornamen = "Test",
                Geslachtsnaam = "Aaa",
                Voorvoegsel = "van den"
            },
            GeheimhoudingPersoonsgegevens = 0,
            VerblijfplaatsInOnderzoek = new GbaInOnderzoek(),
            Geslacht = new Waardetabel
            {
                Code = "M",
                Omschrijving = "man"
            }
        };
    }

    public static GbaBewoner CreateDefaultMetNaam(string voornamen, string geslachtsnaam)
    {
        return new GbaBewoner
        {
            Burgerservicenummer = "123456789",
            Geboorte = new GbaGeboorteBeperkt
            {
                Datum = "19900101"
            },
            Naam = new GbaNaamBasis
            {
                AdellijkeTitelPredicaat = null,
                Voornamen = voornamen,
                Geslachtsnaam = geslachtsnaam,
                Voorvoegsel = "van den"
            },
            GeheimhoudingPersoonsgegevens = 0,
            VerblijfplaatsInOnderzoek = new GbaInOnderzoek(),
            Geslacht = new Waardetabel
            {
                Code = "M",
                Omschrijving = "man"
            }
        };
    }
}