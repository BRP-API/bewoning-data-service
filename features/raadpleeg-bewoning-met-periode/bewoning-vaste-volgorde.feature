# language: nl
@api @valideer-volgorde
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
      |                              0800 |                           20250101 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250401 |
    En de persoon met burgerservicenummer '000000036' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250126 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250401 |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250103 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250401 |
    En de persoon met burgerservicenummer '000000072' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250201 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250000 |
    En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250201 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250301 |
    En de persoon met burgerservicenummer '000000060' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250201 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      |                              0800 |                           20250000 |

  Regel: Elke wijziging van de samenstelling van bewoners van een adresseerbaar object leidt tot een bewoning

    Scenario: een nieuwe bewoner (000000024) verhuist naar het adres in de gevraagde periode, wordt dit een extra bewoning
      Als bewoningen wordt gezocht met de volgende parameters
        | naam                             | waarde             |
        | type                             | BewoningMetPeriode |
        | datumVan                         |         2025-01-26 |
        | datumTot                         |         2026-02-01 |
        | adresseerbaarObjectIdentificatie |   0800010000000001 |
      Dan heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2025-01-26 tot 2025-02-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000012 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000024 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000036 |
      En heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2025-02-01 tot 2025-02-02 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000012 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000024 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000036 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000048 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000060 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000072 |
      En heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2025-02-02 tot 2025-03-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000012 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000024 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000036 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000048 |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000060 |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000072 |
      En heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2025-03-01 tot 2025-04-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000012 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000024 |
      En heeft de bewoning een bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000036 |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000060 |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000072 |
      En heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2025-04-01 tot 2026-01-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000060 |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
        | burgerservicenummer |
        |           000000072 |
