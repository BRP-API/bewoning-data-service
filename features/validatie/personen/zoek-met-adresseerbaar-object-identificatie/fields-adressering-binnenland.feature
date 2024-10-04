#language: nl

@input-validatie
Functionaliteit: adresregels vragen met adresseringBinnenland fields alias bij zoeken op adresseerbaar object identificatie

  Abstract Scenario: de fields parameter bevat adressering veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010051001502                        |
    | fields                           | burgerservicenummer,<fields>            |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                            |
    | adresseringBinnenland             |
    | adresseringBinnenland.adresregel1 |
    | adresseringBinnenland.adresregel2 |

  @fout-case
  Abstract Scenario: De fields parameter bevat een niet-bestaand gezag veld
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010051001502                        |
    | fields                           | <fields>                                |
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

    Voorbeelden:
    | fields                                  |
    | adresseringBinnenland.adresregel3       |
    | adresseringBinnenland.land              |
    | adresseringBinnenland.land.code         |
    | adresseringBinnenland.land.omschrijving |
