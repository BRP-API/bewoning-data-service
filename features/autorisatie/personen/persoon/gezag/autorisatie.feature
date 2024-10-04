# language: nl

@autorisatie
Functionaliteit: autorisatie gegevens van gezag van Persoon

    @fout-case
    Abstract Scenario: Afnemer vraagt om veld <fields> waarvoor deze niet geautoriseerd is
      Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
      | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
      | 10120 113210 113310             | N                        | 20201128                |
      En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | naam         | waarde |
      | afnemerID    | 000008 |
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | 000000012                       |
      | fields              | <fields>                        |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                                  |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3             |
      | title    | U bent niet geautoriseerd voor één of meerdere opgegeven field waarden. |
      | status   | 403                                                                     |
      | code     | unauthorizedField                                                       |
      | instance | /haalcentraal/api/brp/personen                                          |

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

    Abstract Scenario: Afnemer vraagt <fields>, en heeft uitsluitend de autorisatie die nodig is om deze vraag te mogen stellen
      Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
      | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
      | 10120 PAGZ01                    | N                        | 20201128                |
      En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | naam         | waarde |
      | afnemerID    | 000008 |
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | 000000012                       |
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
