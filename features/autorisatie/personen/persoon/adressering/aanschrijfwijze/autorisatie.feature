# language: nl

@autorisatie
Functionaliteit: autorisatie voor aanschrijfwijze

  Regel: Vragen met fields om aanschrijfwijze, wanneer de gebruiker niet geautoriseerd is voor alle velden waaruit aanschrijfwijze wordt afgeleid, geeft een foutmelding
  
    Abstract Scenario: Afnemer vraagt om <fields> en heeft uitsluitend de autorisatie die nodig is om deze vraag te mogen stellen
      Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
      | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
      | 10120 <minimale autorisatie>    | N                        | 20201128                |
      En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | naam         | waarde |
      | afnemerID    | 000008 |
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | 000000024                       |
      | fields              | <fields>                        |
      Dan heeft de response 0 personen

      Voorbeelden:
      | fields                                              | minimale autorisatie |
      | adressering.aanschrijfwijze                         | PANM04 PANM05        |
      | adressering.aanschrijfwijze.aanspreekvorm           | PANM04               |
      | adressering.aanschrijfwijze.naam                    | PANM05               |
      | adresseringBinnenland.aanschrijfwijze               | PANM04 PANM05        |
      | adresseringBinnenland.aanschrijfwijze.aanspreekvorm | PANM04               |
      | adresseringBinnenland.aanschrijfwijze.naam          | PANM05               |

    @fout-case
    Abstract Scenario: Afnemer vraagt om <fields> en is niet geautoriseerd voor <missende autorisatie>
      Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
      | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
      | <ad hoc rubrieken>              | N                        | 20201128                |
      En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | naam         | waarde |
      | afnemerID    | 000008 |
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | 000000024                       |
      | fields              | <fields>                        |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                                  |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3             |
      | title    | U bent niet geautoriseerd voor één of meerdere opgegeven field waarden. |
      | status   | 403                                                                     |
      | code     | unauthorizedField                                                       |
      | instance | /haalcentraal/api/brp/personen                                          |

      Voorbeelden:
      | fields                                              | ad hoc rubrieken                                                               | missende autorisatie                    |
      | adressering.aanschrijfwijze                         | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710        | aanspreekvorm (PANM04) en naam (PANM05) |
      | adressering.aanschrijfwijze                         | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710 PANM05 | aanspreekvorm (PANM04)                  |
      | adressering.aanschrijfwijze                         | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710 PANM04 | naam (PANM05)                           |
      | adressering.aanschrijfwijze.aanspreekvorm           | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710 PANM05 | aanspreekvorm (PANM04)                  |
      | adressering.aanschrijfwijze.naam                    | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710 PANM04 | naam (PANM05)                           |
      | adresseringBinnenland.aanschrijfwijze               | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710        | aanspreekvorm (PANM04) en naam (PANM05) |
      | adresseringBinnenland.aanschrijfwijze               | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710 PANM05 | aanspreekvorm (PANM04)                  |
      | adresseringBinnenland.aanschrijfwijze               | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710 PANM04 | naam (PANM05)                           |
      | adresseringBinnenland.aanschrijfwijze.aanspreekvorm | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710 PANM05 | aanspreekvorm (PANM04)                  |
      | adresseringBinnenland.aanschrijfwijze.naam          | 10120 10210 10220 10230 10240 10410 16110 50220 50230 50240 50610 50710 PANM04 | naam (PANM05)                           |
