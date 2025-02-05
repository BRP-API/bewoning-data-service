# language: nl
@api
Functionaliteit: De sortering van mogelijke bewoners op vaste volgorde
Een mogelijke bewoner is een persoon die (voor een deel) van de gevraagde periode in de onzekerheidsperiode van de gevraagde adreshouding valt

  Achtergrond:
    Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0800 |                         0800010000000001 |

  Regel: (Mogelijke) bewoners worden op basis van datum aanvang adreshouding oplopend gesorteerd zodat de eerste (mogelijke) bewoner op het adres als eerste in de (mogelijke) bewoners lijst staat.

    @valideer-volgorde
    Scenario: De mogelijke bewoners worden gesorteerd op datum aanvang adreshouding
      Gegeven de persoon met burgerservicenummer '000000012' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250100 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250000 |
      Als bewoningen wordt gezocht met de volgende parameters
        | naam                             | waarde             |
        | type                             | BewoningMetPeriode |
        | datumVan                         |         2025-01-01 |
        | datumTot                         |         2025-02-01 |
        | adresseerbaarObjectIdentificatie |   0800010000000001 |
      Dan heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2025-01-01 tot 2025-02-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning mogelijke bewoners met de volgende gegevens
        | burgerservicenummer |
        |           000000024 |
        |           000000012 |

  Regel: (Mogelijke) bewoners worden op basis van geslachtsnaam alfabetisch gesorteerd als de datum aanvang adreshouding van de bewoners overeenkomen

    @valideer-volgorde
    Scenario: De mogelijke bewoners worden alfabetisch gesorteerd op geslachtsnaam
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | geslachtsnaam (02.40) |
        | Jansen                |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250200 |
      En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | geslachtsnaam (02.40) |
        | Boer                  |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250200 |
      Als bewoningen wordt gezocht met de volgende parameters
        | naam                             | waarde             |
        | type                             | BewoningMetPeriode |
        | datumVan                         |         2025-02-01 |
        | datumTot                         |         2025-03-01 |
        | adresseerbaarObjectIdentificatie |   0800010000000001 |
      Dan heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2025-02-01 tot 2025-03-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning mogelijke bewoners met de volgende gegevens
        | burgerservicenummer | naam.geslachtsnaam |
        |           000000024 | Boer               |
        |           000000012 | Jansen             |

  Regel: (Mogelijke) bewoners worden op basis van voornamen alfabetisch gesorteerd als de datum aanvang adreshouding en de geslachtsnaam van de bewoners overeenkomen

    @valideer-volgorde
    Scenario: De bewoners worden alfabetisch gesorteerd op voornamen
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | voornamen (02.10) |
        | Bert              |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250300 |
      En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | voornamen (02.10) |
        | Arie              |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250300 |
      Als bewoningen wordt gezocht met de volgende parameters
        | naam                             | waarde             |
        | type                             | BewoningMetPeriode |
        | datumVan                         |         2025-03-01 |
        | datumTot                         |         2025-04-01 |
        | adresseerbaarObjectIdentificatie |   0800010000000001 |
      Dan heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2025-03-01 tot 2025-04-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning mogelijke bewoners met de volgende gegevens
        | burgerservicenummer | naam.voornamen |
        |           000000024 | Arie           |
        |           000000012 | Bert           |

  Regel: (Mogelijke) bewoners worden op basis van geboortedatum oplopend (van oud naar jong) gesorteerd als de datum aanvang adreshouding,geslachtsnaam en voornamen van de bewoners overeenkomen

    @valideer-volgorde
    Scenario: De bewoners worden oplopend gesorteerd op geboortedatum (van oud naar jong)
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | geboortedatum (03.10) |
        | vandaag - 35 jaar     |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250300 |
      En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | geboortedatum (03.10) |
        | vandaag - 50 jaar     |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250300 |
      Als bewoningen wordt gezocht met de volgende parameters
        | naam                             | waarde             |
        | type                             | BewoningMetPeriode |
        | datumVan                         |         2025-03-01 |
        | datumTot                         |         2025-04-01 |
        | adresseerbaarObjectIdentificatie |   0800010000000001 |
      Dan heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2025-03-01 tot 2025-04-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning mogelijke bewoners met de volgende gegevens
        | burgerservicenummer | geboorte.datum    |
        |           000000024 | vandaag - 50 jaar |
        |           000000012 | vandaag - 35 jaar |
