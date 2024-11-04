            #language: nl

            @api
            Functionaliteit: raadpleeg bewoning in periode levert geboortedatum bewoner

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
            | geboortedatum (03.10) |
            | 19630405              |
            En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
            | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
            | 0800                              | 20100818                           |

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
            | burgerservicenummer | geboorte.datum |
            | 000000024           | 19630405       |