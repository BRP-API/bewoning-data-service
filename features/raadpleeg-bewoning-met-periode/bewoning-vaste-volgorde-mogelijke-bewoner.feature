# language: nl
@api
Functionaliteit: De samenstelling van bewoners in een periode is gesorteerd op datum aanvang adreshouding en naam

  Achtergrond:
    Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0800 |                         0800010000000001 |
    En adres 'A2' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0800 |                         0800010000000002 |
    En de persoon met burgerservicenummer '000000012' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250100 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250201 |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250000 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250201 |
    En de persoon met burgerservicenummer '000000036' heeft de volgende gegevens
      | geslachtsnaam (02.40) |
      | Jansen                |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250200 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250301 |
    En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
      | geslachtsnaam (02.40) |
      | Boer                  |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250200 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250301 |
    En de persoon met burgerservicenummer '000000060' heeft de volgende gegevens
      | geslachtsnaam (02.40) | geboortedatum (03.10) |
      | Pietersen             | vandaag - 35 jaar     |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250300 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250401 |
    En de persoon met burgerservicenummer '000000072' heeft de volgende gegevens
      | geslachtsnaam (02.40) | geboortedatum (03.10) |
      | Pietersen             | vandaag - 50 jaar     |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250300 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250401 |

  Regel: Binnen een bewoning samenstelling worden de bewoners in een vaste volgorde gesorteerd

    @valideer-volgorde
    Scenario: De bewoners worden gesorteerd op datum aanvang adreshouding
        Bewoner met bsn 000000012 is later ingeschreven dan de persoon met bsn 000000024
        Binnen de gevraagde periode zijn beide personen bewoners van het adresseerbaar object
        Omdat persoon met bsn 000000024 eerder is ingeschreven op dit adres, wordt deze persoon eerder vermeld in de response

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
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000024 |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000012 |

    @valideer-volgorde
    Scenario: De bewoners worden gesorteerd op geslachtsnaam als datum aanvang adreshouding overeenkomt
        Beide bewoners in de gevraagde periode zijn op dezelfde dag ingeschreven op het adresseerbaar object
        De bewoners worden alfabetisch gesorteerd op geslachtsnaam

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
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer | naam.geslachtsnaam |
        |           000000048 | Boer               |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer | naam.geslachtsnaam |
        |           000000036 | Jansen             |

    @valideer-volgorde
    Scenario: De bewoners worden gesorteerd op geboortedatum als datum aanvang aadreshouding en geslachtsnaam overeenkomt
        Beide bewoners in de gevraagde periode zijn op dezelfde dag ingeschreven op het adresseerbaar object
        Beide bewoners hebben dezelfde geslachtsnaam
        De bewoners worden gesorteerd op geboortedatum, van oud naar jong

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
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer | naam.geslachtsnaam | geboorte.datum    |
        |           000000072 | Pietersen          | vandaag - 50 jaar |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer | naam.geslachtsnaam | geboorte.datum    |
        |           000000060 | Pietersen          | vandaag - 35 jaar |
