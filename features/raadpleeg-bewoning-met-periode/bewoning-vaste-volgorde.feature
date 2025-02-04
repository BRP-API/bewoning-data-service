# language: nl
@api
Functionaliteit: De sortering van bewoners op vaste volgorde

  Achtergrond:
    Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0800 |                         0800010000000001 |
    En adres 'A2' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0800 |                         0800010000000002 |

  Regel: Binnen een bewoning samenstelling worden de bewoners in een vaste volgorde gesorteerd.
   De sortering gebeurt op basis van de volgende criteria, in deze volgorde:
    1. Datum van aanvang adreshouding: bewoners worden gesorteerd op de datum waarop zij op het adres zijn ingeschreven.
    2. Geslachtsnaam: als meerdere bewoners dezelfde aanvangsdatum hebben, worden zij alfabetisch gesorteerd op hun achternaam.
    3. Voornamen: bij gelijke achternamen wordt verder gesorteerd op voornamen in alfabetische volgorde.
    4. Geboortedatum: als de voornamen ook gelijk zijn, wordt tenslotte gesorteerd op geboortedatum, waarbij de oudste persoon eerst komt.

    @valideer-volgorde
    Scenario: De bewoners worden gesorteerd op datum aanvang adreshouding
        Bewoner met bsn 000000012 is later ingeschreven dan de persoon met bsn 000000024
        Binnen de gevraagde periode zijn beide personen bewoners van het adresseerbaar object
        Omdat persoon met bsn 000000024 eerder is ingeschreven op dit adres, wordt deze persoon eerder vermeld in de response

      Gegeven de persoon met burgerservicenummer '000000012' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250101 |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250201 |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20240101 |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250201 |
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
      En heeft de bewoning bewoners met de volgende gegevens
        | burgerservicenummer |
        |           000000024 |
        |           000000012 |

    @valideer-volgorde
    Scenario: De bewoners worden gesorteerd op geslachtsnaam als datum aanvang adreshouding overeenkomt
        Beide bewoners in de gevraagde periode zijn op dezelfde dag ingeschreven op het adresseerbaar object
        De bewoners worden alfabetisch gesorteerd op geslachtsnaam

      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | geslachtsnaam (02.40) |
        | Jansen                |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250201 |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250301 |
      En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | geslachtsnaam (02.40) |
        | Boer                  |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250201 |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250301 |
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
      En heeft de bewoning bewoners met de volgende gegevens
        | burgerservicenummer | naam.geslachtsnaam |
        |           000000024 | Boer               |
        |           000000012 | Jansen             |

    @valideer-volgorde
    Scenario: De bewoners worden gesorteerd op geboortedatum als datum aanvang adreshouding en geslachtsnaam overeenkomt
        Beide bewoners in de gevraagde periode zijn op dezelfde dag ingeschreven op het adresseerbaar object
        Beide bewoners hebben dezelfde geslachtsnaam
        De bewoners worden alfabetisch gesorteerd op voornaam

      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | voornamen (02.10) |
        | Bert              |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250301 |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250401 |
      En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | voornamen (02.10) |
        | Arie              |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250301 |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250401 |
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
      En heeft de bewoning bewoners met de volgende gegevens
        | burgerservicenummer | naam.voornamen |
        |           000000024 | Arie           |
        |           000000012 | Bert           |

    @valideer-volgorde
    Scenario: De bewoners worden gesorteerd op geboortedatum als datum aanvang adreshouding en geslachtsnaam overeenkomt
        Beide bewoners in de gevraagde periode zijn op dezelfde dag ingeschreven op het adresseerbaar object
        Beide bewoners hebben dezelfde geslachtsnaam
        Beide bewoners hebben dezelfde voornamen
        De bewoners worden gesorteerd op geboortedatum, van oud naar jong

      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
        | geboortedatum (03.10) |
        | vandaag - 35 jaar     |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250301 |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250401 |
      En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
        | geboortedatum (03.10) |
        | vandaag - 50 jaar     |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250301 |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
        | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
        |                              0800 |                           20250401 |
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
      En heeft de bewoning bewoners met de volgende gegevens
        | burgerservicenummer | geboorte.datum    |
        |           000000024 | vandaag - 50 jaar |
        |           000000012 | vandaag - 35 jaar |
