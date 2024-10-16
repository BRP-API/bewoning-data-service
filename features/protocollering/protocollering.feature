#language: nl

Functionaliteit: leveren van de pl id van de (mogelijke) bewoners van een adresseerbaar object op een peildatum/in een periode

  Achtergrond:
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000001                         |
    En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | pl_id |
    | 2001  |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20200818                           |
    En de persoon met burgerservicenummer '000000036' heeft de volgende gegevens
    | pl_id |
    | 2002  |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20210818                           |
    En de persoon met burgerservicenummer '000000048' heeft de volgende gegevens
    | pl_id |
    | 2003  |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20210900                           |

  Regel: in de response wordt een komma gescheiden lijst van de pl_id's van de geleverde (mogelijke) bewoners opgenomen in de response header "x-geleverde-pls"

    Scenario: gevraagde peildatum levert 1 bewoner
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | adresseerbaarObjectIdentificatie | 0800010000000001     |
      | peildatum                        | 2021-01-01           |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2021-01-01 tot 2021-01-02 |
      | adresseerbaarObjectIdentificatie | 0800010000000001          |
      En heeft de bewoning een bewoner met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |
      En de response headers is gelijk aan
      | naam            | waarde |
      | x-geleverde-pls | 2001   |

    Scenario: gevraagde peildatum levert meerdere bewoners
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | adresseerbaarObjectIdentificatie | 0800010000000001     |
      | peildatum                        | 2021-08-18           |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2021-08-18 tot 2021-08-19 |
      | adresseerbaarObjectIdentificatie | 0800010000000001          |
      En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |
      | 000000036           |
      En de response headers is gelijk aan
      | naam            | waarde    |
      | x-geleverde-pls | 2001,2002 |

    Scenario: gevraagde peildatum levert meerdere bewoners en mogelijke bewoners
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | adresseerbaarObjectIdentificatie | 0800010000000001     |
      | peildatum                        | 2021-09-18           |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2021-09-18 tot 2021-09-19 |
      | adresseerbaarObjectIdentificatie | 0800010000000001          |
      En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |
      | 000000036           |
      En heeft de bewoning mogelijke bewoners met de volgende gegevens
      | burgerservicenummer |
      | 000000048           |
      En de response headers is gelijk aan
      | naam            | waarde         |
      | x-geleverde-pls | 2001,2002,2003 |

  Regel: de pl_id van een bewoner wordt éénmaal geleverd. Ook als de bewoner in verschillende perioden op het adres staat ingeschreven

    Scenario: gevraagde periode levert een bewoner meerdere keren
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | adresseerbaarObjectIdentificatie | 0800010000000001   |
      | datumVan                         | 2021-08-17         |
      | datumTot                         | 2021-08-19         |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2021-08-17 tot 2021-08-18 |
      | adresseerbaarObjectIdentificatie | 0800010000000001          |
      En heeft de bewoning een bewoner met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |
      En heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2021-08-18 tot 2021-08-19 |
      | adresseerbaarObjectIdentificatie | 0800010000000001          |
      En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |
      | 000000036           |
      En de response headers is gelijk aan
      | naam            | waarde    |
      | x-geleverde-pls | 2001,2002 |
