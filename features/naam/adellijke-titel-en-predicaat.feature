#language: nl

@api
Functionaliteit: raadpleeg bewoning levert adellijke titel en predicaat bewoner

Als provider van de bewoning informatie service
wil ik dat de adellijke titel en predicaat van de bewoner wordt geleverd
zodat ik de volledige naam van de bewoner kan leveren aan de consumer van de Bewoning API

  Achtergrond:
  Gegeven adres 'A1' heeft de volgende gegevens
  | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
  | 0800                 | 0800000000000001                         |

Regel: Adellijke titel en predicaat van bewoner wordt geleverd bij raadplegen van bewoning

  Abstract Scenario: adellijke titel en predicaat van bewoner wordt geleverd bij het raadplegen van bewoning met periode
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | naam                                 | waarde                    |
    | adellijke titel of predicaat (02.20) | <adellijkeTitelPredicaat> |
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
    | adellijkeTitelPredicaat.code         | <adellijkeTitelPredicaat>    |
    | adellijkeTitelPredicaat.omschrijving | <adellijkeTitelOmschrijving> |
    | adellijkeTitelPredicaat.soort        | <adellijkeTitelSoort>        |

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

  Abstract Scenario: adellijke titel en predicaat van bewoner wordt geleverd bij het raadplegen van bewoning met peildatum
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | naam                                 | waarde                    |
    | adellijke titel of predicaat (02.20) | <adellijkeTitelPredicaat> |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde               |
    | type                             | BewoningMetPeildatum |
    | peildatum                         | 2010-09-01          |
    | adresseerbaarObjectIdentificatie | 0800000000000001     |
    Dan heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2010-09-02 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een bewoner met de volgende gegevens
    | burgerservicenummer |
    | 000000024           |
    En heeft de bewoner de volgende 'naam' gegevens
    | naam                                 | waarde                       |
    | adellijkeTitelPredicaat.code         | <adellijkeTitelPredicaat>    |
    | adellijkeTitelPredicaat.omschrijving | <adellijkeTitelOmschrijving> |
    | adellijkeTitelPredicaat.soort        | <adellijkeTitelSoort>        |

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