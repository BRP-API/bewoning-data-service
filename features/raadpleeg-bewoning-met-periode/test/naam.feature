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
      En de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | voornamen (02.10) | adellijke titel of predicaat (02.20) | voorvoegsel (02.30) | geslachtsnaam (02.40) |
      | Carolina          | BS                                   | Van                 | Naersen               |
      En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      | 0800                              | 20100818                           |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      | 0800                              | 20160526                           |
      En de persoon is vervolgens ingeschreven op een buitenlands adres met de volgende gegevens
      | land (13.10) | datum aanvang adres buitenland (13.20) | regel 1 adres buitenland (13.30) | regel 2 adres buitenland (13.40) | regel 3 adres buitenland (13.50) |
      | 5010         | 20230526                               | Rue du pomme 26                  | Bruxelles                        | postcode 1000                    |
      En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      | 0800                              | 20140808                           |
      En de persoon met burgerservicenummer '000000012' is ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      | 0800                              | 20081103                           |
      En de persoon is vervolgens ingeschreven op een buitenlands adres met de volgende gegevens
      | land (13.10) | datum aanvang adres buitenland (13.20) | regel 1 adres buitenland (13.30) | regel 2 adres buitenland (13.40) | regel 3 adres buitenland (13.50) |
      | 5010         | 20160428                               | Rue des poires 36                | Bruxelles                        | postcode 1000                    |

  Regel: een persoon is binnen een periode bewoner van een adresseerbaar object als:
  - de van datum van de periode valt op of na datum aanvang adreshouding van de persoon op het adresseerbaar object
  - de tot datum van de periode valt vóór datum aanvang adreshouding van de persoon op het volgende adresseerbaar object

  Scenario: bewoning wordt gevraagd voor een periode die ligt binnen de verblijfperiode van één persoon op het adresseerbaar object
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
      | voornamen                            | Carolina  |
      | adellijkeTitelPredicaat.code         | BR        |
      | adellijkeTitelPredicaat.omschrijving | barones   |
      | adellijkeTitelPredicaat.soort        | predicaat |
      | voorvoegsel                          | Van       |
      | geslachtsnaam                        | Naersen   |