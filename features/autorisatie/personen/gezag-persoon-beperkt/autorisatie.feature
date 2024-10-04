# language: nl

@autorisatie
Functionaliteit: autorisatie gegevens van Persoon bij zoeken

  @fout-case
  Abstract Scenario: Afnemer vraagt om veld <fields> waarvoor deze niet geautoriseerd is
    Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
    | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
    | 81180                           | N                        | 20201128                |
    En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
    | naam      | waarde |
    | afnemerID | 000008 |
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010051001502                        |
    | fields                           | <fields>                                |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                                  |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3             |
    | title    | U bent niet geautoriseerd voor één of meerdere opgegeven field waarden. |
    | status   | 403                                                                     |
    | code     | unauthorizedField                                                       |
    | instance | /haalcentraal/api/brp/personen                                          |

    Voorbeelden:
    | fields                | missende autorisatie |
    | burgerservicenummer   | 10120                |
    | geslacht              | 10410                |
    | geslacht.code         | 10410                |
    | geslacht.omschrijving | 10410                |

  Abstract Scenario: Afnemer vraagt <fields>, en heeft uitsluitend de autorisatie die nodig is om deze vraag te mogen stellen
    Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
    | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
    | 81180 <ad hoc rubrieken>        | N                        | 20201128                |
    En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
    | naam      | waarde |
    | afnemerID | 000008 |
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010051001502                        |
    | fields                           | <fields>                                |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                | ad hoc rubrieken |
    | burgerservicenummer   | 10120            |
    | geslacht              | 10410            |
    | geslacht.code         | 10410            |
    | geslacht.omschrijving | 10410            |
