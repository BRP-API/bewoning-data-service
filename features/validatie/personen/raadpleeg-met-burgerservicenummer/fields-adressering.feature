#language: nl

@input-validatie
Functionaliteit: adressering velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat adressering veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                    |
    | adressering                               |
    | adressering.adresregel1                   |
    | adressering.adresregel2                   |
    | adressering.adresregel3                   |
    | adressering.land                          |
    | adressering.land.code                     |
    | adressering.land.omschrijving             |
    | adressering.aanhef                        |
    | adressering.aanschrijfwijze               |
    | adressering.aanschrijfwijze.aanspreekvorm |
    | adressering.aanschrijfwijze.naam          |
    | adressering.gebruikInLopendeTekst         |

  @fout-case
  Scenario: De fields parameter bevat een niet-bestaand adressering veld
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | adressering.bestaatNiet         |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                      |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
    | title    | Een of meerdere parameters zijn niet correct.               |
    | status   | 400                                                         |
    | detail   | De foutieve parameter(s) zijn: fields[0].                   |
    | code     | paramsValidation                                            |
    | instance | /haalcentraal/api/brp/personen                              |
    En heeft de response invalidParams met de volgende gegevens
    | code   | name      | reason                                       |
    | fields | fields[0] | Parameter bevat een niet bestaande veldnaam. |
