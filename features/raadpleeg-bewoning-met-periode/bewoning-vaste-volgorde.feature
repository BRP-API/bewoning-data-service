# language: nl
@api
Functionaliteit: De sortering van bewoners op vaste volgorde

  Achtergrond:
    Gegeven adres 'A1' met identificatiecode verblijfplaats '0800010000000001' en gemeentecode '0518'
    En de persoon 'P4' met burgerservicenummer '000000012'
    En de persoon 'P3' met burgerservicenummer '000000024'
    En de persoon 'P2' met burgerservicenummer '000000036'
    En de persoon 'P1' met burgerservicenummer '000000048'

  Regel: (Mogelijke) bewoners worden op basis van datum aanvang adreshouding oplopend gesorteerd zodat de eerste (mogelijke) bewoner op het adres als eerste in de (mogelijke) bewoners lijst staat.

    @valideer-volgorde
    Scenario: De bewoners in de gevraagde periode verschillende datum aanvang adreshouding en datum aanvang is volledig bekend
      Gegeven 'P1' is ingeschreven op adres 'A1' op '20250101'
      En 'P2' is ingeschreven op adres 'A1' op '20240101'
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
        | burgerservicenummer | naam.geslachtsnaam |
        |           000000036 | P2                 |
        |           000000048 | P1                 |

    @valideer-volgorde
    Scenario: De mogelijke bewoners in de gevraagde periode hebben verschillende datum aanvang en datum aanvang is deels onbekend
      Gegeven 'P1' is ingeschreven op adres 'A1' op '20250100'
      En 'P2' is ingeschreven op adres 'A1' op '20250000'
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
        | burgerservicenummer | naam.geslachtsnaam |
        |           000000036 | P2                 |
        |           000000048 | P1                 |

  Regel: (Mogelijke) bewoners worden op basis van geslachtsnaam alfabetisch gesorteerd als de datum aanvang adreshouding overeenkomt

    @valideer-volgorde
    Scenario: De bewoners in de gevraagde periode hebben verschillende geslachtsnaam en datum aanvang is volledig bekend
      Gegeven 'P1, P2, P3' en 'P4' zijn ingeschreven op adres 'A1' op '20230101'
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
      En heeft de bewoning bewoners met de volgende gegevens
        | burgerservicenummer | naam.geslachtsnaam |
        |           000000048 | P1                 |
        |           000000036 | P2                 |
        |           000000024 | P3                 |
        |           000000012 | P4                 |

    @valideer-volgorde
    Scenario: De mogelijke bewoners in de gevraagde periode hebben verschillende geslachtsnaam en datum aanvang deels onbekend
      Gegeven 'P1, P2, P3' en 'P4' zijn ingeschreven op adres 'A1' op '20230000'
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
        |           000000048 | P1                 |
        |           000000036 | P2                 |
        |           000000024 | P3                 |
        |           000000012 | P4                 |

  Regel: (Mogelijke) bewoners worden op basis van voornamen alfabetisch gesorteerd als de datum aanvang adreshouding en de geslachtsnaam van de bewoners overeenkomen

    @valideer-volgorde
    Scenario: De bewoners in de gevraagde periode hebben verschillende voornaam en datum aanvang is volledig bekend
      Gegeven persoon 'P1'
      * heeft de volgende gegevens
        | geslachtsnaam (02.40) | voornamen (02.10) |
        | Alberts               | Bert              |
      En persoon 'P2'
      * heeft de volgende gegevens
        | geslachtsnaam (02.40) | voornamen (02.10) |
        | Alberts               | Arie              |
      En 'P1' en 'P2' zijn ingeschreven op adres 'A1' op '20220101'
      Als bewoningen wordt gezocht met de volgende parameters
        | naam                             | waarde             |
        | type                             | BewoningMetPeriode |
        | datumVan                         |         2022-01-01 |
        | datumTot                         |         2023-01-01 |
        | adresseerbaarObjectIdentificatie |   0800010000000001 |
      Dan heeft de response een bewoning met de volgende gegevens
        | naam                             | waarde                    |
        | periode                          | 2022-01-01 tot 2023-01-01 |
        | adresseerbaarObjectIdentificatie |          0800010000000001 |
      En heeft de bewoning bewoners met de volgende gegevens
        | burgerservicenummer | naam.geslachtsnaam | naam.voornamen |
        |           000000036 | Alberts            | Arie           |
        |           000000048 | Alberts            | Bert           |

    @valideer-volgorde
    Scenario: De mogelijke bewoners in de gevraagde periode hebben verschillende voornaam en datum aanvang is deels onbekend
      Gegeven persoon 'P1'
      * heeft de volgende gegevens
        | geslachtsnaam (02.40) | voornamen (02.10) |
        | Alberts               | Bert              |
      En persoon 'P2'
      * heeft de volgende gegevens
        | geslachtsnaam (02.40) | voornamen (02.10) |
        | Alberts               | Arie              |
      En 'P1' en 'P2' zijn ingeschreven op adres 'A1' op '20230000'
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
        | burgerservicenummer | naam.geslachtsnaam | naam.voornamen |
        |           000000036 | Alberts            | Arie           |
        |           000000048 | Alberts            | Bert           |

  Regel: (Mogelijke) bewoners worden op basis van geboortedatum oplopend (van oud naar jong) gesorteerd als de datum aanvang adreshouding,geslachtsnaam en voornamen van de bewoners overeenkomen

    @valideer-volgorde
    Scenario: De bewoners in de gevraagde priode hebben verschillende geboortedatum en datum aanvang is volledig bekend
      Gegeven persoon 'P1'
      * heeft de volgende gegevens
        | geslachtsnaam (02.40) | geboortedatum (03.10) |
        | Alberts               | vandaag - 35 jaar     |
      Gegeven persoon 'P2'
      * heeft de volgende gegevens
        | geslachtsnaam (02.40) | geboortedatum (03.10) |
        | Alberts               | vandaag - 50 jaar     |
      En 'P1' en 'P2' zijn ingeschreven op adres 'A1' op '20220101'
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
        | burgerservicenummer | naam.geslachtsnaam | geboorte.datum    |
        |           000000036 | Alberts            | vandaag - 50 jaar |
        |           000000048 | Alberts            | vandaag - 35 jaar |


    @valideer-volgorde
    Scenario: De mogelijke bewoners in de gevraagde priode hebben verschillende geboortedatum en datum aanvang is deels onbekend
      Gegeven persoon 'P1'
      * heeft de volgende gegevens
        | geslachtsnaam (02.40) | geboortedatum (03.10) |
        | Alberts               | vandaag - 35 jaar     |
      Gegeven persoon 'P2'
      * heeft de volgende gegevens
        | geslachtsnaam (02.40) | geboortedatum (03.10) |
        | Alberts               | vandaag - 50 jaar     |
      En 'P1' en 'P2' zijn ingeschreven op adres 'A1' op '20250000'
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
        | burgerservicenummer | naam.geslachtsnaam | geboorte.datum    |
        |           000000036 | Alberts            | vandaag - 50 jaar |
        |           000000048 | Alberts            | vandaag - 35 jaar |