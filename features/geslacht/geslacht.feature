#language: nl

@api
Functionaliteit: raadpleeg bewoning levert geslachtsaanduiding bewoner

Als provider van de bewoning informatie service
wil ik dat de geslachtsaanduiding van de bewoner wordt geleverd
zodat ik de volledige naam van de bewoner kan leveren aan de consumer van de Bewoning API

  Achtergrond:
  Gegeven adres 'A1' heeft de volgende gegevens
  | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
  | 0800                 | 0800000000000001                         |

  Regel: Geslachtsaanduiding van bewoner wordt geleverd bij raadplegen van bewoning

  Scenario: geslachtsaanduiding van bewoner wordt geleverd bij het raadplegen van bewoning met periode
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | geslachtsaanduiding (04.10) |
    | M                           |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2010-09-01         |
    | datumTot                         | 2014-08-01         |
    | adresseerbaarObjectIdentificatie | 0800000000000001   |
    Dan heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2014-08-01 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een bewoner met de volgende gegevens
    | burgerservicenummer |
    | 000000024           |
    En heeft de bewoner de volgende 'geslacht' gegevens
    | naam         | waarde |
    | code         | M      |
    | omschrijving | man    |

  Scenario: geslachtsaanduiding van bewoner wordt geleverd bij het raadplegen van bewoning met peildatum
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | geslachtsaanduiding (04.10) |
    | V                           |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde               |
    | type                             | BewoningMetPeildatum |
    | peildatum                        | 2010-09-01           |
    | adresseerbaarObjectIdentificatie | 0800000000000001     |
    Dan heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2010-09-02 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een bewoner met de volgende gegevens
    | burgerservicenummer |
    | 000000024           |
    En heeft de bewoner de volgende 'geslacht' gegevens
    | naam         | waarde |
    | code         | V      |
    | omschrijving | vrouw  |

  Regel: Geslachtsaanduiding van mogelijke bewoner wordt geleverd bij raadplegen van bewoning in onzekerheidsperiode

  Scenario: geslachtsaanduiding van mogelijke bewoner wordt geleverd bij het raadplegen van bewoning met periode in onzekerheidsperiode
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | geslachtsaanduiding (04.10) |
    | V                           |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100000                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2010-09-01         |
    | datumTot                         | 2011-01-01         |
    | adresseerbaarObjectIdentificatie | 0800000000000001   |
    En heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2011-01-01 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een mogelijke bewoner met de volgende gegevens
    | burgerservicenummer | geslacht.code | geslacht.omschrijving |
    | 000000024           | V             | vrouw                 |

  Scenario: geslachtsaanduiding van mogelijke bewoner wordt geleverd bij het raadplegen van bewoning met peildatum in onzekerheidsperiode
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | geslachtsaanduiding (04.10) |
    | M                           |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100000                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde               |
    | type                             | BewoningMetPeildatum |
    | peildatum                        | 2010-09-01           |
    | adresseerbaarObjectIdentificatie | 0800000000000001     |
    En heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2010-09-02 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een mogelijke bewoner met de volgende gegevens
    | burgerservicenummer | geslacht.code | geslacht.omschrijving |
    | 000000024           | M             | man                   |
