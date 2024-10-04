# language: nl

@autorisatie
Functionaliteit: autorisatie verblijfplaatshistorie raadplegen met peildatum

  Regel: Wanneer verblijfplaatshistorie wordt geraadpleegd waarvoor de gebruiker niet geautoriseerd is, wordt een foutmelding gegeven
    Om verblijfplaatshistorie te mogen vragen moet de afnemer geautoriseerd zijn voor virtuele rubriek PX.VP.07

    Scenario: Afnemer vraagt verblijfplaatshistorie, en heeft uitsluitend de autorisatie die nodig is om deze vraag te mogen stellen
      Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
      | Rubrieknummer ad hoc (35.95.60) | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
      | PXVP07                          | N                        | 20201128                |
      En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | naam      | waarde |
      | afnemerID | 000008 |
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | 000000012             |
      | peildatum           | 2023-05-26            |
      Dan heeft de response 0 verblijfplaatsen

    @fout-case
    Scenario: Afnemer vraagt verblijfplaatshistorie, maar is niet geautoriseerd voor PX.VP.07 en wel voor alle losse rubrieken
      Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
      | Rubrieknummer ad hoc (35.95.60)                                                                                                                                                                                                                                | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
      | 10120 80910 81010 81110 81115 81120 81130 81140 81150 81160 81170 81180 81190 81210 81310 81330 81340 81350 580910 581010 581110 581115 581120 581130 581140 581150 581160 581170 581180 581190 581210 581310 581330 581340 581350 PAVP01 PAVP02 PAVP03 PAVP04 | N                        | 20201128                |
      En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | naam      | waarde |
      | afnemerID | 000008 |
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | 000000012             |
      | peildatum           | 2023-05-26            |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3 |
      | title    | U bent niet geautoriseerd voor het gebruik van deze API.    |
      | status   | 403                                                         |
      | detail   | Niet geautoriseerd voor verblijfplaatshistorie.             |
      | code     | unauthorized                                                |
      | instance | /haalcentraal/api/brphistorie/verblijfplaatshistorie        |


  Regel: Een gemeente als afnemer is geautoriseerd voor alle verblijfplaats gegevens
    Wanneer gemeenteCode in de 'claim' in de OAuth token gevuld is,
    dan wordt niet gekeken naar de autorisatie van de afnemer

    Scenario: Gemeente vraagt om verblijfplaatshistorie en afnemer is niet geautoriseerd voor verblijfplaatshistorie (PX.VP.07)
      Gegeven de afnemer met indicatie '000008' heeft de volgende 'autorisatie' gegevens
      | Rubrieknummer ad hoc (35.95.60)                                                                                                                                                                                                                   | Medium ad hoc (35.95.67) | Datum ingang (35.99.98) |
      | 10120 80910 81010 81115 81120 81130 81140 81150 81160 81170 81180 81190 81210 81310 81330 81340 81350 580910 581110 581115 581120 581130 581140 581150 581160 581170 581180 581190 581210 581310 581330 581340 581350 PAVP01 PAVP02 PAVP03 PAVP04 | N                        | 20201128                |
      En de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | naam         | waarde |
      | afnemerID    | 000008 |
      | gemeenteCode | 0800   |
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | 000000012             |
      | peildatum           | 2023-05-26            |
      Dan heeft de response 0 verblijfplaatsen
