      #language: nl

      @api
      Functionaliteit: raadpleeg bewoning in periode levert naam bewoner

      Als consumer van de Bewoning API
      wil ik kunnen opvragen welke personen in een periode op een adresseerbaar object verblijven/hebben verbleven
      zodat ik deze informatie kan gebruiken in mijn proces

      Achtergrond:
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0800                 | 0800000000000001                         |
      En adres 'A2' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0800                 | 0800000000000002                         |

  Regel: een persoon is binnen een periode bewoner van een adresseerbaar object als:
  - de van datum van de periode valt op of na datum aanvang adreshouding van de persoon op het adresseerbaar object
  - de tot datum van de periode valt vóór datum aanvang adreshouding van de persoon op het volgende adresseerbaar object

  Scenario: bewoning wordt gevraagd voor een periode die ligt binnen de verblijfperiode van één persoon op het adresseerbaar object en persoonsgegevens zijn in onderzoek
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | voornamen (02.10) | adellijke titel of predicaat (02.20) | voorvoegsel (02.30) | geslachtsnaam (02.40) | aanduiding in onderzoek (83.10) | datum ingang onderzoek (83.20) |
      | Carolina          | BS                                   | Van                 | Naersen               | 010000                          | 20230101                       |
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
      | naam                                 | waarde   |
      | voornamen                            | Carolina |
      | adellijkeTitelPredicaat.code         | BS       |
      | adellijkeTitelPredicaat.omschrijving | barones  |
      | adellijkeTitelPredicaat.soort        | titel    |
      | voorvoegsel                          | Van      |
      | geslachtsnaam                        | Naersen  |
      En heeft de bewoner de volgende 'persoonInOnderzoek' gegevens
      | naam                          | waarde   |
      | aanduidingGegevensInOnderzoek | 010000   |
      | datumIngangOnderzoek          | 20230101 |

      Abstract Scenario: bewoning wordt gevraagd voor een periode die ligt binnen de verblijfperiode van één persoon op het adresseerbaar object
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | naam                                 | waarde                    |
      | voornamen (02.10)                    | <voornamen>               |
      | adellijke titel of predicaat (02.20) | <adellijkeTitelPredicaat> |
      | voorvoegsel (02.30)                  | <voorvoegsel>             |
      | geslachtsnaam (02.40)                | <geslachtsnaam>           |
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
      | voornamen                            | <voornamen>                  |
      | voorvoegsel                          | <voorvoegsel>                |
      | geslachtsnaam                        | <geslachtsnaam>              |
      | adellijkeTitelPredicaat.code         | <adellijkeTitelPredicaat>    |
      | adellijkeTitelPredicaat.omschrijving | <adellijkeTitelOmschrijving> |
      | adellijkeTitelPredicaat.soort        | <adellijkeTitelSoort>        |

      Voorbeelden:
      | voornamen | voorvoegsel | geslachtsnaam | adellijkeTitelPredicaat | adellijkeTitelOmschrijving | adellijkeTitelSoort |
      | Robin Sam | van den     | Aedel         | B                       | baron                      | titel               |
      | Robin Sam | van den     | Aedel         | BS                      | barones                    | titel               |
      | Robin Sam | van den     | Aedel         | G                       | graaf                      | titel               |
      | Robin Sam | van den     | Aedel         | GI                      | gravin                     | titel               |
      | Robin Sam | van den     | Aedel         | H                       | hertog                     | titel               |
      | Robin Sam | van den     | Aedel         | HI                      | hertogin                   | titel               |
      | Robin Sam | van den     | Aedel         | M                       | markies                    | titel               |
      | Robin Sam | van den     | Aedel         | MI                      | markiezin                  | titel               |
      | Robin Sam | van den     | Aedel         | P                       | prins                      | titel               |
      | Robin Sam | van den     | Aedel         | PS                      | prinses                    | titel               |
      | Robin Sam | van den     | Aedel         | R                       | ridder                     | titel               |
      | Robin Sam | van den     | Aedel         | R                       | ridder                     | titel               |