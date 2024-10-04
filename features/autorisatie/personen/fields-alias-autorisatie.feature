#language: nl

@autorisatie
Functionaliteit: adressering - autorisatie bij gebruik van de fields alias

  Achtergrond:
    Gegeven de geauthenticeerde consumer heeft de volgende 'claim' gegevens
    | afnemerID |
    | 000008    |

Regel: de 'adresseringBinnenland' field alias moet worden gebruikt door een consumer die niet is geautoriseerd voor het bevragen van adresregels horende bij verblijfplaats buitenland

  @fout-case
  Abstract Scenario: afnemer is niet geautoriseerd voor 'adressering buitenland' en vraagt zonder de fields alias één of meer adresregel velden van buitenlandse adressen
    Gegeven de afnemer met indicatie '000008' is geautoriseerd voor 'adressering binnenland' gegevens
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000097                       |
    | fields              | <field>                         |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                                  |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3             |
    | title    | U bent niet geautoriseerd voor één of meerdere opgegeven field waarden. |
    | status   | 403                                                                     |
    | code     | unauthorizedField                                                       |
    | instance | /haalcentraal/api/brp/personen                                          |

    Voorbeelden:
    | field                   |
    | adressering             |
    | adressering.adresregel3 |
    | adressering.land        |

  @fout-case
  Abstract Scenario: afnemer is niet geautoriseerd voor 'adressering buitenland' en vraagt bij het zoeken met adresseerbaar object identificatie zonder de fields alias één of meer adresregel velden van buitenlandse adressen
    Gegeven de afnemer met indicatie '000008' is geautoriseerd voor 'adressering binnenland' gegevens
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010051001502                        |
    | fields                           | <field>                                 |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                                  |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3             |
    | title    | U bent niet geautoriseerd voor één of meerdere opgegeven field waarden. |
    | status   | 403                                                                     |
    | code     | unauthorizedField                                                       |
    | instance | /haalcentraal/api/brp/personen                                          |

    Voorbeelden:
    | field                   |
    | adressering             |
    | adressering.adresregel3 |
    | adressering.land        |

  @fout-case
  Abstract Scenario: afnemer is niet geautoriseerd voor 'adressering buitenland' en vraagt bij het zoeken met nummeraanduiding identificatie zonder de fields alias één of meer adresregel velden van buitenlandse adressen
    Gegeven de afnemer met indicatie '000008' is geautoriseerd voor 'adressering binnenland' gegevens
    Als personen wordt gezocht met de volgende parameters
    | naam                          | waarde                               |
    | type                          | ZoekMetNummeraanduidingIdentificatie |
    | nummeraanduidingIdentificatie | 0599010051001501                     |
    | fields                        | <field>                              |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                                  |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3             |
    | title    | U bent niet geautoriseerd voor één of meerdere opgegeven field waarden. |
    | status   | 403                                                                     |
    | code     | unauthorizedField                                                       |
    | instance | /haalcentraal/api/brp/personen                                          |

    Voorbeelden:
    | field                   |
    | adressering             |
    | adressering.adresregel3 |
    | adressering.land        |

Regel: de 'verblijfplaatsBinnenland' field alias moet worden gebruikt door een consumer die niet is geautoriseerd voor het bevragen van velden horende bij verblijfplaats buitenland

  @fout-case
  Abstract Scenario: afnemer is niet geautoriseerd voor 'verblijfplaats buitenland' en vraagt zonder de fields alias één of meer verblijfplaats velden
    Gegeven de afnemer met indicatie '000008' is geautoriseerd voor 'verblijfplaats binnenland' gegevens
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000097                       |
    | fields              | <field>                         |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                                  |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3             |
    | title    | U bent niet geautoriseerd voor één of meerdere opgegeven field waarden. |
    | status   | 403                                                                     |
    | code     | unauthorizedField                                                       |
    | instance | /haalcentraal/api/brp/personen                                          |

    Voorbeelden:
    | field                               |
    | verblijfplaats                      |
    | verblijfplaats.verblijfadres        |
    | verblijfplaats.verblijfadres.regel3 |
    | verblijfplaats.verblijfadres.land   |
