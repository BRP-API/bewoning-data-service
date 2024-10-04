#language: nl

@input-validatie
Functionaliteit: adressering - fields alias fout cases

Regel: de 'adresseringBinnenland' field alias kan niet worden gebruikt in combinatie met de adresregel velden voor verblijfplaats buitenland

  @fout-case
  Abstract Scenario: de fields alias 'adresseringBinnenland' wordt gebruikt voor het vragen van één of meerdere buitenland velden van de adressering gegevensgroep
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000097                       |
    | fields              | adresseringBinnenland.<field>   |
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
    | field       |
    | adresregel3 |
    | land        |

Regel: de field alias 'verblijfplaatsBinnenland' mag niet worden gebruikt voor het vragen van 'verblijfplaats buitenland' velden

  @fout-case
  Abstract Scenario: de fields alias 'verblijfplaatsBinnenland' wordt gebruikt voor het vragen van één of meerdere 'verblijfplaats buitenland' velden
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000097                       |
    | fields              | <fields>                        |
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
    | fields                                        |
    | verblijfplaatsBinnenland.verblijfadres.regel1 |
    | verblijfplaatsBinnenland.verblijfadres.regel2 |
    | verblijfplaatsBinnenland.verblijfadres.regel3 |
    | verblijfplaatsBinnenland.verblijfadres.land   |
