      #language: nl

      @api
      Functionaliteit: raadpleeg bewoning levert naam bewoner

      Als provider van de bewoning informatie service
      wil ik dat de naam en de geslachtsaanduiding van de bewoner wordt geleverd
      zodat ik de volledige naam van de bewoner kan leveren aan de consumer van de Bewoning API

      Achtergrond:
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0800                 | 0800000000000001                         |

  Regel: een persoon is binnen een periode bewoner van een adresseerbaar object als:
  - de van datum van de periode valt op of na datum aanvang adreshouding van de persoon op het adresseerbaar object
  - de tot datum van de periode valt vóór datum aanvang adreshouding van de persoon op het volgende adresseerbaar object

  Scenario: naam van de bewoner wordt geleverd bij het raadplegen van bewoning met periode
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | voornamen (02.10) | adellijke titel of predicaat (02.20) | voorvoegsel (02.30) | geslachtsnaam (02.40) | geslachtsaanduiding (04.10) |
      | Robin Sam         | B                                    | van den             | Aedel                 | M                           |
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
      En heeft de bewoner de volgende 'naam' gegevens
      | naam                                 | waarde    |
      | voornamen                            | Robin Sam |
      | adellijkeTitelPredicaat.code         | B         |
      | adellijkeTitelPredicaat.omschrijving | baron     |
      | adellijkeTitelPredicaat.soort        | titel     |
      | voorvoegsel                          | van den   |
      | geslachtsnaam                        | Aedel     |
      En heeft de bewoner de volgende 'geslacht' gegevens
      | naam         | waarde |
      | code         | M      |
      | omschrijving | man    |

  Scenario: naam van de mogelijke bewoner wordt geleverd bij het raadplegen van bewoning met periode
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | voornamen (02.10) | adellijke titel of predicaat (02.20) | voorvoegsel (02.30) | geslachtsnaam (02.40) | geslachtsaanduiding (04.10) |
      | Robin Sam         | B                                    | van den             | Aedel                 | M                           |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      | 0800                              | 20100000                           |
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         | 2010-09-01         |
      | datumTot                         | 2014-08-01         |
      | adresseerbaarObjectIdentificatie | 0800000000000001   |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2011-01-01 tot 2014-08-01 |
      | adresseerbaarObjectIdentificatie | 0800000000000001          |
      En heeft de bewoning een bewoner met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |
      En heeft de bewoner de volgende 'naam' gegevens
      | naam                                 | waarde    |
      | voornamen                            | Robin Sam |
      | adellijkeTitelPredicaat.code         | B         |
      | adellijkeTitelPredicaat.omschrijving | baron     |
      | adellijkeTitelPredicaat.soort        | titel     |
      | voorvoegsel                          | van den   |
      | geslachtsnaam                        | Aedel     |
      En heeft de bewoner de volgende 'geslacht' gegevens
      | naam         | waarde |
      | code         | M      |
      | omschrijving | man    |
      En heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2010-09-01 tot 2011-01-01 |
      | adresseerbaarObjectIdentificatie | 0800000000000001          |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.voorvoegsel | naam.geslachtsnaam | naam.adellijkeTitelPredicaat.code | naam.adellijkeTitelPredicaat.omschrijving | naam.adellijkeTitelPredicaat.soort | geslacht.code | geslacht.omschrijving |
      | 000000024           | Robin Sam      | van den          | Aedel              | B                                 | baron                                     | titel                              | M             | man                   |

  Scenario: naam van de bewoner wordt geleverd bij het raadplegen van bewoning met peildatum
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | voornamen (02.10) | adellijke titel of predicaat (02.20) | voorvoegsel (02.30) | geslachtsnaam (02.40) | geslachtsaanduiding (04.10) |
      | Robin Sam         | B                                    | van den             | Aedel                 | M                           |
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
      En heeft de bewoner de volgende 'naam' gegevens
      | naam                                 | waarde    |
      | voornamen                            | Robin Sam |
      | adellijkeTitelPredicaat.code         | B         |
      | adellijkeTitelPredicaat.omschrijving | baron     |
      | adellijkeTitelPredicaat.soort        | titel     |
      | voorvoegsel                          | van den   |
      | geslachtsnaam                        | Aedel     |
      En heeft de bewoner de volgende 'geslacht' gegevens
      | naam         | waarde |
      | code         | M      |
      | omschrijving | man    |

      Abstract Scenario: adellijke titel en predicaat van de bewoner wordt geleverd bij het raadplegen van bewoning met periode
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | naam                                 | waarde                    |
      | voornamen (02.10)                    | Robin Sam                 |
      | voorvoegsel (02.30)                  | van den                   |
      | geslachtsnaam (02.40)                | Aedel                     |
      | adellijke titel of predicaat (02.20) | <adellijkeTitelPredicaat> |
      | geslachtsaanduiding (04.10)          | <geslachtsAanduiding>     |
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
      En heeft de bewoner de volgende 'naam' gegevens
      | naam                                 | waarde                       |
      | voornamen                            | Robin Sam                    |
      | voorvoegsel                          | van den                      |
      | geslachtsnaam                        | Aedel                        |
      | adellijkeTitelPredicaat.code         | <adellijkeTitelPredicaat>    |
      | adellijkeTitelPredicaat.omschrijving | <adellijkeTitelOmschrijving> |
      | adellijkeTitelPredicaat.soort        | <adellijkeTitelSoort>        |
      En heeft de bewoner de volgende 'geslacht' gegevens
      | naam         | waarde                  |
      | code         | <geslachtsAanduiding>   |
      | omschrijving | <geslachtsOmschrijving> |

      Voorbeelden:
      | adellijkeTitelPredicaat | adellijkeTitelOmschrijving | adellijkeTitelSoort | geslachtsAanduiding | geslachtsOmschrijving |
      | B                       | baron                      | titel               | M                   | man                   |
      | BS                      | barones                    | titel               | V                   | vrouw                 |
      | B                       | baron                      | titel               | O                   | onbekend              |
      | G                       | graaf                      | titel               | M                   | man                   |
      | GI                      | gravin                     | titel               | V                   | vrouw                 |
      | GI                      | gravin                     | titel               | O                   | onbekend              |
      | H                       | hertog                     | titel               | M                   | man                   |
      | HI                      | hertogin                   | titel               | V                   | vrouw                 |
      | M                       | markies                    | titel               | M                   | man                   |
      | MI                      | markiezin                  | titel               | V                   | vrouw                 |
      | P                       | prins                      | titel               | M                   | man                   |
      | PS                      | prinses                    | titel               | V                   | vrouw                 |
      | R                       | ridder                     | titel               | M                   | man                   |
      | R                       | ridder                     | titel               | V                   | vrouw                 |
      | JH                      | jonkheer                   | predicaat           | M                   | man                   |
      | JV                      | jonkvrouw                  | predicaat           | V                   | vrouw                 |