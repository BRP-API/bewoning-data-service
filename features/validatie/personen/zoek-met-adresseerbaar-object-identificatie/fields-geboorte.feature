#language: nl

@input-validatie
Functionaliteit: geboorte velden vragen met fields bij zoeken op adresseerbaar object identificatie

  Abstract Scenario: de fields parameter bevat gezag veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010051001502                        |
    | fields                           | burgerservicenummer,<fields>            |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                     |
    | geboorte                   |
    | geboorte.datum             |
    | geboorte.datum.type        |
    | geboorte.datum.datum       |
    | geboorte.datum.langFormaat |
    | geboorte.datum.onbekend    |
    | geboorte.datum.jaar        |
    | geboorte.datum.maand       |
    | leeftijd                   |

  @fout-case
  Scenario: De fields parameter bevat een niet-bestaand gezag veld
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010051001502                        |
    | fields                           | geboorte.bestaatNiet                    |
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
