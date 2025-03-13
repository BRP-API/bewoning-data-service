# language: nl
@api
Functionaliteit: De sortering van bewoners op vaste volgorde

  Achtergrond:
    Gegeven adres 'A1' met identificatiecode verblijfplaats '0800010000000001' en gemeentecode '0518'
    En de persoon 'Daal' met burgerservicenummer '00000012'
    En de persoon 'Coenen' met burgerservicenummer '00000024'
    En de persoon 'Boer' met burgerservicenummer '00000036'
    En de persoon 'Alberts' met burgerservicenummer '00000048'

  Regel: (Mogelijke) bewoners worden op basis van datum aanvang adreshouding oplopend gesorteerd zodat de eerste (mogelijke) bewoner op het adres als eerste in de (mogelijke) bewoners lijst staat.

    @valideer-volgorde
    Scenario: De mogelijke bewoners in de gevraagde periode hebben verschillende geslachtsnaam en datum aanvang alleen jaar bekend
      Gegeven 'Daal, Coenen, Boer' en 'Alberts' zijn ingeschreven op adres 'A1' op '20230000'
      Als bewoningen wordt gezocht met de volgende parameters
        | naam                             | waarde             |
        | type                             | BewoningMetPeriode |
        | datumVan                         |         2023-01-01 |
        | datumTot                         |         2024-01-01 |
        | adresseerbaarObjectIdentificatie |   0800010000000001 |
      Dan heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2023-01-01 tot 2024-01-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning mogelijke bewoners met de volgende gegevens
        | burgerservicenummer | naam.geslachtsnaam |
        |           000000048 | Alberts            |
        |           000000036 | Boer               |
        |           000000024 | Coenen             |
        |           000000012 | Daal               |
