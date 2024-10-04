# language: nl

@autorisatie
Functionaliteit: autorisatie voor het zoeken van reisdocumenten met een burgerservicenummer
  Autorisatie voor het gebruik van de API gebeurt op twee niveau's:
  1. autorisatie van de gebruiker door de afnemende organisatie
  2. autorisatie van de afnemer (organisatie) door RvIG

  Deze feature beschrijft alleen de autorisatie van de afnemer door RvIG.

  Voorlopig wordt de reisdocumenten bevragen API alleen aangeboden aan gemeenten.

  Autorisatie wordt verkregen met behulp van een OAuth 2 token. 
  Wanneer de afnemer een gemeente is, is er ook een gemeentecode opgenomen in de OAuth token.

  Regel: Een gemeente als afnemer is geautoriseerd voor het zoeken van de reisdocumenten van een ingeschreven persoon

    @fout-case
    Scenario: afnemer is geen gemeente
      Gegeven de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | naam      | waarde |
      | afnemerID | 000008 |
      Als reisdocumenten wordt gezocht met de volgende parameters
      | naam                | waarde                                                                     |
      | type                | ZoekMetBurgerservicenummer                                                 |
      | burgerservicenummer | 000000152                                                                  |
      | fields              | reisdocumentnummer,soort,datumEindeGeldigheid,inhoudingOfVermissing,houder |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3 |
      | title    | U bent niet geautoriseerd voor deze vraag.                  |
      | status   | 403                                                         |
      | detail   | Alleen gemeenten mogen reisdocumenten raadplegen.           |
      | code     | unauthorized                                                |
      | instance | /haalcentraal/api/reisdocumenten/reisdocumenten             |

    Scenario: afnemer is een gemeente
      Gegeven de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | naam         | waarde |
      | afnemerID    | 000008 |
      | gemeenteCode | 0800   |
      Als reisdocumenten wordt gezocht met de volgende parameters
      | naam                | waarde                     |
      | type                | ZoekMetBurgerservicenummer |
      | burgerservicenummer | 000000152                  |
      | fields              | reisdocumentnummer         |
      Dan heeft de response 0 reisdocumenten
