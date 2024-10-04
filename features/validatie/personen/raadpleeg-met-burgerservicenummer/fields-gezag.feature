#language: nl

@input-validatie
Functionaliteit: gezagsrelaties vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat gezag veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                     |
    | gezag                                      |
    | gezag.type                                 |
    | gezag.minderjarige                         |
    | gezag.ouders                               |
    | gezag.ouder                                |
    | gezag.derde                                |
    | gezag.derden                               |
    | gezag.minderjarige.burgerservicenummer     |
    | gezag.ouders.burgerservicenummer           |
    | gezag.ouder.burgerservicenummer            |
    | gezag.derde.burgerservicenummer            |
    | gezag.derden.burgerservicenummer           |
    | gezag.ouders,gezag.ouder                   |
    | gezag.type,gezag.minderjarige,gezag.ouders |

  @fout-case
  Scenario: De fields parameter bevat een niet-bestaand gezag veld
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | gezag.bestaatNiet               |
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
