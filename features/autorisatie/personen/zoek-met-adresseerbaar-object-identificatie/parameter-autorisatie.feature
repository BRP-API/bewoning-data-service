# language: nl

@autorisatie
Functionaliteit: autorisatie op parameters bij ZoekMetAdresseerbaarObjectIdentificatie

Regel: Wanneer een parameter wordt gebruikt van een veld waarvoor de gebruiker niet geautoriseerd is, wordt een foutmelding gegeven

  @fout-case
  Scenario: Gebruik van parameter adresseerbaarObjectIdentificatie waarvoor de afnemer niet geautoriseerd is (81180)
    Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
    | Rubrieknummer ad hoc (35.95.60)                       | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
    | 10120 80910 81010 81110 81120 81130 81140 81150 81160 | N                        | 20201128                |
    En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
    | naam         | waarde |
    | afnemerID    | 000008 |
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0800010000219679                        |
    | fields                           | burgerservicenummer                     |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                                                         |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3                                    |
    | title    | U bent niet geautoriseerd voor de gebruikte parameter(s).                                      |
    | status   | 403                                                                                            |
    | detail   | U bent niet geautoriseerd voor het gebruik van parameter(s): adresseerbaarObjectIdentificatie. |
    | code     | unauthorizedParameter                                                                          |
    | instance | /haalcentraal/api/brp/personen                                                                 |

  @fout-case
  Scenario: Gebruik van parameter gemeenteVanInschrijving waarvoor de afnemer niet geautoriseerd is (80910)
    Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
    | Rubrieknummer ad hoc (35.95.60)                       | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
    | 10120 81010 81110 81120 81130 81140 81150 81160 81180 | N                        | 20201128                |
    En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
    | naam         | waarde |
    | afnemerID    | 000008 |
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010000618922                        |
    | gemeenteVanInschrijving          | 0599                                    |
    | fields                           | burgerservicenummer                     |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                                                |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3                           |
    | title    | U bent niet geautoriseerd voor de gebruikte parameter(s).                             |
    | status   | 403                                                                                   |
    | detail   | U bent niet geautoriseerd voor het gebruik van parameter(s): gemeenteVanInschrijving. |
    | code     | unauthorizedParameter                                                                 |
    | instance | /haalcentraal/api/brp/personen                                                        |

  Scenario: Afnemer zoekt met alleen adresseerbaarObjectIdentificatie en heeft uitsluitend de autorisatie die nodig is om deze vraag te mogen stellen
    Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
    | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
    | 10120 81180                     | N                        | 20201128                |
    En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
    | naam         | waarde |
    | afnemerID    | 000008 |
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010000618922                        |
    | fields                           | burgerservicenummer                     |
    Dan heeft de response 0 personen

  Scenario: Afnemer zoekt met adresseerbaarObjectIdentificatie en gemeenteVanInschrijving en heeft uitsluitend de autorisatie die nodig is om deze vraag te mogen stellen
    Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
    | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
    | 10120 80910 81180               | N                        | 20201128                |
    En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
    | naam         | waarde |
    | afnemerID    | 000008 |
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010000618922                        |
    | gemeenteVanInschrijving          | 0599                                    |
    | fields                           | burgerservicenummer                     |
    Dan heeft de response 0 personen

Regel: Een gemeente als afnemer is geautoriseerd voor alle zoekvragen
  Wanneer gemeenteCode in de 'claim' in de OAuth token gevuld is,
  dan wordt niet gekeken naar de autorisatie van de afnemer

  Scenario: Gemeente is niet geautoriseerd voor de zoekparameters
    Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
    | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
    | 10120                           | N                        | 20201128                |
    En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
    | naam         | waarde |
    | afnemerID    | 000008 |
    | gemeenteCode | 0800   |
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010000618922                        |
    | fields                           | burgerservicenummer                     |
    Dan heeft de response 0 personen
