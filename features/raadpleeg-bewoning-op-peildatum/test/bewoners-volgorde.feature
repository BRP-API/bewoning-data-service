# language: nl
@api
Functionaliteit: Sorteren van bewoners op datum aanvang adreshouding, geslachtsnaam, voornamen en geboortedatum
    De sortering gebeurt op basis van de volgende criteria, in deze volgorde:
    1. Datum van aanvang adreshouding: bewoners worden gesorteerd op de datum waarop zij op het adres zijn ingeschreven.
    2. Geslachtsnaam: als meerdere bewoners dezelfde aanvangsdatum hebben, worden zij alfabetisch gesorteerd op hun achternaam.
    3. Voornamen: bij gelijke achternamen wordt verder gesorteerd op voornamen in alfabetische volgorde.
    4. Geboortedatum: als de voornamen ook gelijk zijn, wordt tenslotte gesorteerd op geboortedatum, waarbij de oudste persoon eerst komt.


    Testen door combinaties van bewoners:
    - met verschillende datum aanvang adreshouding
    - met zelfde datum aanvang adreshouding maar verschillende geslachtsnaam
    - met zelfde datum aanvang adreshouding en geslachtsnaam maar verschillende voornamen
    - met zelfde datum aanvang adreshouding en geslachtsnaam en voornamen maar verschillende geboortedatum

    Voor de zekerheid hebben niet-relevante gegevens steeds omgekeerde volgorde. Zou dus het geteste criterium niet goed werken, dan merken we dat aan de sortering op de volgende aspecten.

    Sortering wordt getest voor bewoners en voor mogelijke bewoners.
    Sortering wordt getest met volledige datums en met onvolledige datums.
    Sortering wordt getest met ook verschillende vertrekdatum. Dit mag geen effect hebben op de volgorde.

  Achtergrond:
    Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0800 |                         0800010000000001 |
    En adres 'A2' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0800 |                         0800010000000002 |
    # Zelfde datum aanvang, sorteer op geslachtsnaam
    Gegeven met datum aanvang adreshouding 20150503 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Femke             |              19980526 |
      |                   000000024 | Jansen                | Karel             |              20011014 |
    # Latere datum aanvang
    Gegeven met datum aanvang adreshouding 20180219 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000036 | Boer                  | Elise             |              19971130 |
    # Zelfde datum aanvang en geslachtsnaam, sorteer op voornamen
    Gegeven met datum aanvang adreshouding 20210814 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000048 | Jansen                | Karel Jan         |              19990624 |
      |                   000000061 | Jansen                | Irene             |              20000328 |
    # Zelfde datum aanvang, geslachtsnaam en voornamen, sorteer op geboortedatum
    Gegeven met datum aanvang adreshouding 20230316 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000073 | Pietersen             | Bas               |              20190917 |
      |                   000000085 | Pietersen             | Bas               |              20000119 |

  @valideer-volgorde
  Scenario: Sorteren op geslachtsnaam bij gelijke datum aanvang
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2016-01-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2016-01-01 tot 2016-01-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |

  @valideer-volgorde
  Scenario: Sorteren op datum aanvang adreshouding
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2019-01-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2019-01-01 tot 2019-01-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |
      |           000000036 | Elise          | Boer               |       19971130 |

  @valideer-volgorde
  Scenario: Sorteren op voornamen bij gelijke datum aanvang adreshouding en geslachtsnaam
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2022-01-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2022-01-01 tot 2022-01-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |
      |           000000036 | Elise          | Boer               |       19971130 |
      |           000000061 | Irene          | Jansen             |       20000328 |
      |           000000048 | Karel Jan      | Jansen             |       19990624 |

  @valideer-volgorde
  Scenario: Sorteren op geboortedatum bij gelijke datum aanvang adreshouding, geslachtsnaam en voornamen
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2023-07-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-07-01 tot 2023-07-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |
      |           000000036 | Elise          | Boer               |       19971130 |
      |           000000061 | Irene          | Jansen             |       20000328 |
      |           000000048 | Karel Jan      | Jansen             |       19990624 |
      |           000000085 | Bas            | Pietersen          |       20000119 |
      |           000000073 | Bas            | Pietersen          |       20190917 |

  @valideer-volgorde
  Scenario: Sorteren van mogelijke bewoners, datum aanvang alleen jaar bekend
    # Datum aanvang alleen jaar bekend
    Gegeven met datum aanvang adreshouding 20230000 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Teunissen             | Peter             |              20210411 |
      |                   000000103 | Teunissen             | Leonie            |              20181103 |
      |                   000000115 | Teunissen             | Peter             |              19960523 |
      |                   000000127 | Alberts               | Ruud              |              20160703 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2023-07-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000002 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-07-01 tot 2023-07-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000002 |
    En heeft de bewoning mogelijke bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000127 | Ruud           | Alberts            |       20160703 |
      |           000000103 | Leonie         | Teunissen          |       20181103 |
      |           000000115 | Peter          | Teunissen          |       19960523 |
      |           000000097 | Peter          | Teunissen          |       20210411 |

  @valideer-volgorde
  Scenario: Sorteren van mogelijke bewoners met verschillende bekende of gedeeltelijk onbekende datums aanvang
    # Datum aanvang alleen maand en jaar bekend
    Gegeven met datum aanvang adreshouding 20230400 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000139 | Alberts               | Corine            |              20171109 |
      |                   000000140 | Boer                  | Anton             |              20140621 |
    # Datum aanvang alleen jaar bekend
    En met datum aanvang adreshouding 20230000 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Teunissen             | Peter             |              20210411 |
      |                   000000103 | Teunissen             | Leonie            |              20181103 |
      |                   000000115 | Teunissen             | Peter             |              19960523 |
      |                   000000127 | Alberts               | Ruud              |              20160703 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2023-04-10 |
      | adresseerbaarObjectIdentificatie |     0800010000000002 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-04-10 tot 2023-04-11 |
      | adresseerbaarObjectIdentificatie |          0800010000000002 |
    En heeft de bewoning mogelijke bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000127 | Ruud           | Alberts            |       20160703 |
      |           000000103 | Leonie         | Teunissen          |       20181103 |
      |           000000115 | Peter          | Teunissen          |       19960523 |
      |           000000097 | Peter          | Teunissen          |       20210411 |
      |           000000139 | Corine         | Alberts            |       20171109 |
      |           000000140 | Anton          | Boer               |       20140621 |

  @valideer-volgorde
  Scenario: Sorteren van bewoners met verschillende bekende of gedeeltelijk onbekende datums aanvang
    # Datum aanvang alleen maand en jaar bekend
    Gegeven met datum aanvang adreshouding 20230400 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000139 | Alberts               | Corine            |              20171109 |
      |                   000000140 | Boer                  | Anton             |              20140621 |
    # Datum aanvang alleen jaar bekend
    En met datum aanvang adreshouding 20230000 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Teunissen             | Peter             |              20210411 |
      |                   000000103 | Teunissen             | Leonie            |              20181103 |
      |                   000000115 | Teunissen             | Peter             |              19960523 |
      |                   000000127 | Alberts               | Ruud              |              20160703 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2024-01-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2024-01-01 tot 2024-01-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |
      |           000000036 | Elise          | Boer               |       19971130 |
      |           000000061 | Irene          | Jansen             |       20000328 |
      |           000000048 | Karel Jan      | Jansen             |       19990624 |
      |           000000127 | Ruud           | Alberts            |       20160703 |
      |           000000103 | Leonie         | Teunissen          |       20181103 |
      |           000000115 | Peter          | Teunissen          |       19960523 |
      |           000000097 | Peter          | Teunissen          |       20210411 |
      |           000000085 | Bas            | Pietersen          |       20000119 |
      |           000000073 | Bas            | Pietersen          |       20190917 |
      |           000000139 | Corine         | Alberts            |       20171109 |
      |           000000140 | Anton          | Boer               |       20140621 |

  @valideer-volgorde
  Scenario: Datum vertrek beïnvloed de volgorde niet
    Gegeven vervolgens zijn de volgende personen met datum aanvang adreshouding 20240601 ingeschreven in gemeente '0800' op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Femke             |              19980526 |
      |                   000000048 | Jansen                | Karel Jan         |              19990624 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2023-07-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-07-01 tot 2023-07-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |
      |           000000036 | Elise          | Boer               |       19971130 |
      |           000000061 | Irene          | Jansen             |       20000328 |
      |           000000048 | Karel Jan      | Jansen             |       19990624 |
      |           000000085 | Bas            | Pietersen          |       20000119 |
      |           000000073 | Bas            | Pietersen          |       20190917 |

  @valideer-volgorde
  Scenario: Sorteren mogelijke bewoners met bekende en gedeeltelijk onbekende datum aanvang
    # sommige bewoners zijn mogelijke bewoner door onbekende datum aanvang
    # andere bewoners zijn mogelijk bewoner door onbekende datum aanvang volgende (onbekende vertrekdatum)
    # Datum aanvang alleen jaar bekend
    Gegeven met datum aanvang adreshouding 20230000 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Teunissen             | Peter             |              20210411 |
      |                   000000103 | Teunissen             | Leonie            |              20181103 |
      |                   000000115 | Teunissen             | Peter             |              19960523 |
      |                   000000127 | Alberts               | Ruud              |              20160703 |
    # Datum vertrek is gedeeltelijk onbekend bij personen met volledig bekende datum aanvang
    En vervolgens zijn de volgende personen met datum aanvang adreshouding 20230700 ingeschreven in gemeente '0800' op adres 'A2'
      | burgerservicenummer (01.20) |
      |                   000000061 |
      |                   000000024 |
      |                   000000036 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2023-07-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-07-01 tot 2023-07-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000012 | Femke          | Pietersen          |       19980526 |
      |           000000048 | Karel Jan      | Jansen             |       19990624 |
      |           000000085 | Bas            | Pietersen          |       20000119 |
      |           000000073 | Bas            | Pietersen          |       20190917 |
    En heeft de bewoning mogelijke bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000036 | Elise          | Boer               |       19971130 |
      |           000000061 | Irene          | Jansen             |       20000328 |
      |           000000127 | Ruud           | Alberts            |       20160703 |
      |           000000103 | Leonie         | Teunissen          |       20181103 |
      |           000000115 | Peter          | Teunissen          |       19960523 |
      |           000000097 | Peter          | Teunissen          |       20210411 |

  @valideer-volgorde
  Abstract Scenario: Sorteren bewoner met gedeeltelijk onbekende datum aanvang en bekende aanvang vorige verblijfplaats
    Gegeven met datum aanvang adreshouding <datum aanvang vorige Cees> in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Nielson               | Aart              |              20020411 |
    En vervolgens zijn de volgende personen met datum aanvang adreshouding <datum aanvang Cees> ingeschreven in gemeente '0800' op adres 'A2'
      | burgerservicenummer (01.20) |
      |                   000000097 |
    En met datum aanvang adreshouding <datum aanvang Gerda> in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000103 | Nielson               | Gerda             |              20020411 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2024-01-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000002 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2024-01-01 tot 2024-01-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000002 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000103 | Gerda          | Nielson            |       20020411 |
      |           000000097 | Aart           | Nielson            |       20020411 |

    Voorbeelden:
      | datum aanvang vorige Cees | datum aanvang Cees | datum aanvang Gerda |
      |                  20230612 |           20230000 |            20230603 |
      |                  20230612 |           20230000 |            20230612 |
      |                  20230612 |           20230600 |            20230612 |

  @valideer-volgorde
  Abstract Scenario: Sorteren mogelijke bewoners met gedeeltelijk onbekende datum aanvang en bekende aanvang vorige verblijfplaats
    Gegeven met datum aanvang adreshouding <datum aanvang vorige Cees> in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Nielson               | Aart              |              20020411 |
    En vervolgens zijn de volgende personen met datum aanvang adreshouding <datum aanvang Cees> ingeschreven in gemeente '0800' op adres 'A2'
      | burgerservicenummer (01.20) |
      |                   000000097 |
    Gegeven met datum aanvang adreshouding <datum aanvang vorige Gerda> in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000103 | Nielson               | Gerda             |              20020411 |
    En vervolgens zijn de volgende personen met datum aanvang adreshouding <datum aanvang Gerda> ingeschreven in gemeente '0800' op adres 'A2'
      | burgerservicenummer (01.20) |
      |                   000000103 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2023-07-15 |
      | adresseerbaarObjectIdentificatie |     0800010000000002 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-07-15 tot 2023-07-16 |
      | adresseerbaarObjectIdentificatie |          0800010000000002 |
    En heeft de bewoning mogelijke bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000103 | Gerda          | Nielson            |       20020411 |
      |           000000097 | Aart           | Nielson            |       20020411 |

    Voorbeelden:
      | datum aanvang vorige Cees | datum aanvang Cees | datum aanvang vorige Gerda | datum aanvang Gerda |
      |                  20230712 |           20230000 |                   20230711 |            20230000 |
      |                  20230712 |           20230000 |                   20230700 |            20230000 |
      |                  20230712 |           20230700 |                   20230711 |            20230700 |

  @valideer-volgorde
  Scenario: Sorteren mogelijke bewoners met gedeeltelijk onbekende datum aanvang en bekende aanvang vorige verblijfplaats
    # vorige verblijfplaats
    Gegeven met datum aanvang adreshouding 20230415 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Alberts               | Corine            |              20171109 |
      |                   000000140 | Boer                  | Anton             |              20140621 |
    # deze personen hebben een eerdere datum aanvang vorige verblijfplaats
    En met datum aanvang adreshouding 20230312 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000139 | Teunissen             | Peter             |              20210411 |
      |                   000000103 | Teunissen             | Leonie            |              20181103 |
    En vervolgens zijn de volgende personen met datum aanvang adreshouding 20230000 ingeschreven in gemeente '0800' op adres 'A1'
      | burgerservicenummer (01.20) |
      |                   000000097 |
      |                   000000103 |
      |                   000000139 |
      |                   000000140 |
    En met datum aanvang adreshouding 20230415 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000115 | Alberts               | Ruud              |              20160703 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2023-05-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-05-01 tot 2023-05-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |
      |           000000036 | Elise          | Boer               |       19971130 |
      |           000000061 | Irene          | Jansen             |       20000328 |
      |           000000048 | Karel Jan      | Jansen             |       19990624 |
      |           000000085 | Bas            | Pietersen          |       20000119 |
      |           000000073 | Bas            | Pietersen          |       20190917 |
      |           000000115 | Ruud           | Alberts            |       20160703 |
    En heeft de bewoning mogelijke bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000103 | Leonie         | Teunissen          |       20181103 |
      |           000000139 | Peter          | Teunissen          |       20210411 |
      |           000000097 | Corine         | Alberts            |       20171109 |
      |           000000140 | Anton          | Boer               |       20140621 |

  @valideer-volgorde
  Scenario: Sorteren bewoners met gedeeltelijk onbekende datum aanvang en bekende aanvang vorige verblijfplaats
    # vorige verblijfplaats
    Gegeven met datum aanvang adreshouding 20230415 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Alberts               | Corine            |              20171109 |
      |                   000000140 | Boer                  | Anton             |              20140621 |
    # deze personen hebben een eerdere datum aanvang vorige verblijfplaats
    En met datum aanvang adreshouding 20230312 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000139 | Teunissen             | Peter             |              20210411 |
      |                   000000103 | Teunissen             | Leonie            |              20181103 |
    En vervolgens zijn de volgende personen met datum aanvang adreshouding 20230000 ingeschreven in gemeente '0800' op adres 'A1'
      | burgerservicenummer (01.20) |
      |                   000000097 |
      |                   000000103 |
      |                   000000139 |
      |                   000000140 |
      |                   000000127 |
    En met datum aanvang adreshouding 20230415 in gemeente '0800' zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000115 | Alberts               | Ruud              |              20160703 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde               |
      | type                             | BewoningMetPeildatum |
      | peildatum                        |           2024-01-01 |
      | adresseerbaarObjectIdentificatie |     0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2024-01-01 tot 2024-01-02 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |
      |           000000036 | Elise          | Boer               |       19971130 |
      |           000000061 | Irene          | Jansen             |       20000328 |
      |           000000048 | Karel Jan      | Jansen             |       19990624 |
      |           000000103 | Leonie         | Teunissen          |       20181103 |
      |           000000139 | Peter          | Teunissen          |       20210411 |
      |           000000085 | Bas            | Pietersen          |       20000119 |
      |           000000073 | Bas            | Pietersen          |       20190917 |
      |           000000115 | Ruud           | Alberts            |       20160703 |
      |           000000097 | Corine         | Alberts            |       20171109 |
      |           000000140 | Anton          | Boer               |       20140621 |
