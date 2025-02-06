# language: nl
@api
Functionaliteit: Sorteren van bewoners op datum aanvang adreshouding, geslachtsnaam, voornamen en geboortedatum
    Testen sortering met combinaties van bewoners:
    - met verschillende datum aanvang adreshouding
      * volledig bekende datums
      * gedeeltelijk onbekende datums
      * gedeeltelijk onbekende datums met datum aanvang vorige in de onzekerheidsperiode: volgorde wordt berekend met datum vorige + 1
    - met zelfde datum aanvang adreshouding maar verschillende geslachtsnaam
    - met zelfde datum aanvang adreshouding en geslachtsnaam maar verschillende voornamen
    - met zelfde datum aanvang adreshouding en geslachtsnaam en voornamen maar verschillende geboortedatum
    - met ook verschillende vertrekdatum. Dit mag geen effect hebben op de volgorde

    Sortering wordt getest voor: 
    - bewoners
    - mogelijke bewoners omdat datum aanvang (gedeeltelijk) onbekend is
    - mogelijke bewoners omdat datum aanvang volgende (gedeeltelijk) onbekend is

  Achtergrond:
    Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0800 |                         0800010000000001 |
    En adres 'A2' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      |                 0800 |                         0800010000000002 |

  @valideer-volgorde
  Scenario: Sorteren op geslachtsnaam bij gelijke datum aanvang
    Gegeven met datum aanvang adreshouding 20150503 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Femke             |              19980526 |
      |                   000000024 | Jansen                | Karel             |              20011014 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         |         2016-01-01 |
      | datumTot                         |         2017-01-01 |
      | adresseerbaarObjectIdentificatie |   0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2016-01-01 tot 2017-01-01 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |

  @valideer-volgorde
  Scenario: Sorteren op datum aanvang adreshouding en daarna op geslachtsnaam
    Gegeven met datum aanvang adreshouding 20150503 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Femke             |              19980526 |
      |                   000000024 | Jansen                | Karel             |              20011014 |
    En met datum aanvang adreshouding 20180219 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000036 | Boer                  | Elise             |              19971130 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         |         2019-01-01 |
      | datumTot                         |         2020-01-01 |
      | adresseerbaarObjectIdentificatie |   0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2019-01-01 tot 2020-01-01 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Karel          | Jansen             |       20011014 |
      |           000000012 | Femke          | Pietersen          |       19980526 |
      |           000000036 | Elise          | Boer               |       19971130 |

  @valideer-volgorde
  Scenario: Sorteren op datum aanvang adreshouding en daarna op geslachtsnaam en daarna op voornamen
    # testgevallen t.o.v. 000000012:
    # 000000024: eerdere geslachtsnaam
    # 000000036: latere datum aanvang
    # 000000048: latere datum aanvang dan 000000036
    # 000000061: zelfde datum aanvang als 000000048, eerdere voornaam dan 000000048
    Gegeven met datum aanvang adreshouding 20150503 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Tineke            |              19980526 |
      |                   000000024 | Jansen                | Piet              |              20011014 |
    # zelfde geslachtsnaam, latere voornaam, maar ook eerdere datum aanvang
    En met datum aanvang adreshouding 20180219 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000036 | Jansen                | Sarah             |              19971130 |
      # twee bewoners met zelfde datum aanvang adreshouding en zelfde geslachtsnaam maar verschillende voornamen
    En met datum aanvang adreshouding 20210814 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000048 | Jansen                | Karel Jan         |              19990624 |
      |                   000000061 | Jansen                | Irene             |              20000328 |
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
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Piet           | Jansen             |       20011014 |
      |           000000012 | Tineke         | Pietersen          |       19980526 |
      |           000000036 | Sarah          | Jansen             |       19971130 |
      |           000000061 | Irene          | Jansen             |       20000328 |
      |           000000048 | Karel Jan      | Jansen             |       19990624 |

  @valideer-volgorde
  Scenario: Sorteren op datum aanvang adreshouding en daarna op geslachtsnaam en daarna op voornamen en daarna op geboortedatum
    # testgevallen t.o.v. 000000012:
    # 000000024: Eerdere datum aanvang, zelfde geslachtsnaam, zelfde voornamen, latere geboortedatum, maar eerdere datum aanvang
    # 000000036: Zelfde datum aanvang, eerdere geslachtsnaam, zelfde voornamen, latere geboortedatum
    # 000000048: Zelfde datum aanvang, zelfde geslachtsnaam, eerdere voornaam, latere geboortedatum
    # 000000073: Zelfde datum aanvang, zelfde geslachtsnaam, zelfde voornamen, eerdere geboortedatum
    # 000000061: Latere datum aanvang, zelfde geslachtsnaam, zelfde voornamen, eerdere geboortedatum
    # 000000085: Latere datum aanvang, eerdere geslachtsnaam, zelfde voornamen, eerdere geboortedatum
    Gegeven met datum aanvang adreshouding 20220503 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000024 | Pietersen             | Bas               |              20210328 |
    En met datum aanvang adreshouding 20230316 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Bas               |              20190917 |
      |                   000000036 | Jansen                | Bas               |              20210328 |
      |                   000000048 | Pietersen             | Annet             |              20210328 |
      |                   000000073 | Pietersen             | Bas               |              19980119 |
    En met datum aanvang adreshouding 20230730 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000061 | Pietersen             | Bas               |              19980119 |
      |                   000000085 | Jansen                | Bas               |              19980119 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         |         2024-01-01 |
      | datumTot                         |         2025-01-01 |
      | adresseerbaarObjectIdentificatie |   0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2024-01-01 tot 2025-01-01 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000024 | Bas            | Pietersen          |       20210328 |
      |           000000036 | Bas            | Jansen             |       20210328 |
      |           000000048 | Annet          | Pietersen          |       20210328 |
      |           000000073 | Bas            | Pietersen          |       19980119 |
      |           000000012 | Bas            | Pietersen          |       20190917 |
      |           000000085 | Bas            | Jansen             |       19980119 |
      |           000000061 | Bas            | Pietersen          |       19980119 |

  @valideer-volgorde
  Scenario: Sorteren van mogelijke bewoners
    # testgevallen t.o.v. 000000012 met datum aanvang alleen jaar bekend:
    # 000000024: Zelfde datum aanvang, zelfde geslachtsnaam, eerdere voornamen, zelfde geboortedatum
    # 000000036: Zelfde datum aanvang, zelfde geslachtsnaam, zelfde voornamen, eerdere geboortedatum
    # 000000048: Zelfde datum aanvang, eerdere geslachtsnaam, zelfde voornamen, zelfde geboortedatum
    # 000000061: Datum aanvang met ook maand bekend in zelfde jaar, zelfde geslachtsnaam, zelfde voornamen, zelfde geboortedatum
    # 000000073: Volledig onbekende datum aanvang, zelfde geslachtsnaam, zelfde voornamen, zelfde geboortedatum
    Gegeven met datum aanvang adreshouding 20230000 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Teunissen             | Peter             |              20210411 |
      |                   000000024 | Teunissen             | Leonie            |              20210411 |
      |                   000000036 | Teunissen             | Peter             |              19960523 |
      |                   000000048 | Alberts               | Peter             |              20210411 |
    En met datum aanvang adreshouding 20230700 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000061 | Teunissen             | Peter             |              20210411 |
    En met datum aanvang adreshouding 00000000 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000073 | Teunissen             | Peter             |              20210411 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         |         2023-07-01 |
      | datumTot                         |         2023-08-01 |
      | adresseerbaarObjectIdentificatie |   0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-07-01 tot 2023-08-01 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning mogelijke bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000073 | Peter          | Teunissen          |       20210411 |
      |           000000048 | Peter          | Alberts            |       20210411 |
      |           000000024 | Leonie         | Teunissen          |       20210411 |
      |           000000036 | Peter          | Teunissen          |       19960523 |
      |           000000012 | Peter          | Teunissen          |       20210411 |
      |           000000061 | Peter          | Teunissen          |       20210411 |
  @valideer-volgorde
  Scenario: Sorteren van bewoners met verschillende bekende of gedeeltelijk onbekende datums aanvang
    # testgevallen t.o.v. 000000012:
    # 000000024: Eerdere datum aanvang, zelfde geslachtsnaam, zelfde voornamen, latere geboortedatum, maar eerdere datum aanvang
    # 000000036: Zelfde datum aanvang, eerdere geslachtsnaam, zelfde voornamen, latere geboortedatum
    # 000000048: Zelfde datum aanvang, zelfde geslachtsnaam, eerdere voornaam, latere geboortedatum
    # 000000073: Zelfde datum aanvang, zelfde geslachtsnaam, zelfde voornamen, eerdere geboortedatum
    # 000000061: Latere datum aanvang, zelfde geslachtsnaam, zelfde voornamen, eerdere geboortedatum
    # 000000085: Latere datum aanvang, eerdere geslachtsnaam, zelfde voornamen, eerdere geboortedatum
    # 000000097: Datum aanvang alleen zelfde jaar bekend, zelfde geslachtsnaam, zelfde voornamen, zelfde geboortedatum
    # 000000103: Datum aanvang alleen zelfde jaar bekend, eerdere geslachtsnaam, zelfde voornamen, zelfde geboortedatum
    # 000000115: Datum aanvang alleen zelfde jaar bekend, zelfde geslachtsnaam, eerdere voornamen, zelfde geboortedatum
    # 000000127: Datum aanvang alleen zelfde jaar bekend, zelfde geslachtsnaam, zelfde voornamen, eerdere geboortedatum
    # 000000139: Datum aanvang alleen zelfde jaar en maand bekend, zelfde geslachtsnaam, zelfde voornamen, zelfde geboortedatum
    Gegeven met datum aanvang adreshouding 20230210 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000024 | Pietersen             | Bas               |              20210328 |
    En met datum aanvang adreshouding 20230316 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Bas               |              20190917 |
      |                   000000036 | Jansen                | Bas               |              20210328 |
      |                   000000048 | Pietersen             | Annet             |              20210328 |
      |                   000000073 | Pietersen             | Bas               |              19980119 |
    En met datum aanvang adreshouding 20230730 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000061 | Pietersen             | Bas               |              19980119 |
      |                   000000085 | Jansen                | Bas               |              19980119 |
    En met datum aanvang adreshouding 20230000 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Pietersen             | Bas               |              20190917 |
      |                   000000103 | Jansen                | Bas               |              20190917 |
      |                   000000115 | Pietersen             | Annet             |              20190917 |
      |                   000000127 | Pietersen             | Bas               |              19980119 |
    En met datum aanvang adreshouding 20230300 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000139 | Pietersen             | Bas               |              20190917 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         |         2024-01-01 |
      | datumTot                         |         2025-01-01 |
      | adresseerbaarObjectIdentificatie |   0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2024-01-01 tot 2025-01-01 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000103 | Bas            | Jansen             |       20190917 |
      |           000000115 | Annet          | Pietersen          |       20190917 |
      |           000000127 | Bas            | Pietersen          |       19980119 |
      |           000000097 | Bas            | Pietersen          |       20190917 |
      |           000000024 | Bas            | Pietersen          |       20210328 |
      |           000000139 | Bas            | Pietersen          |       20190917 |
      |           000000036 | Bas            | Jansen             |       20210328 |
      |           000000048 | Annet          | Pietersen          |       20210328 |
      |           000000073 | Bas            | Pietersen          |       19980119 |
      |           000000012 | Bas            | Pietersen          |       20190917 |
      |           000000085 | Bas            | Jansen             |       19980119 |
      |           000000061 | Bas            | Pietersen          |       19980119 |

  @valideer-volgorde
  Scenario: Datum vertrek beïnvloed de volgorde niet
    Gegeven met datum aanvang adreshouding 20230316 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Bas               |              20190917 |
      |                   000000036 | Jansen                | Bas               |              20210328 |
      |                   000000048 | Pietersen             | Annet             |              20210328 |
      |                   000000073 | Pietersen             | Bas               |              19980119 |
    En vervolgens zijn de volgende personen met datum aanvang adreshouding 20240601 ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Bas               |              20190917 |
      |                   000000048 | Pietersen             | Annet             |              20210328 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         |         2023-07-01 |
      | datumTot                         |         2024-01-01 |
      | adresseerbaarObjectIdentificatie |   0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-07-01 tot 2024-01-01 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000036 | Bas            | Jansen             |       20210328 |
      |           000000048 | Annet          | Pietersen          |       20210328 |
      |           000000073 | Bas            | Pietersen          |       19980119 |
      |           000000012 | Bas            | Pietersen          |       20190917 |

  @valideer-volgorde
  Scenario: Sorteren mogelijke bewoners met gedeeltelijk onbekende datum aanvang en mogelijke bewoners met bekende datum aanvang
    # sommige bewoners zijn mogelijke bewoner door onbekende datum aanvang
    # andere bewoners zijn mogelijk bewoner door onbekende datum aanvang volgende (onbekende vertrekdatum)
    # Testgevallen: zie scenario "Sorteren van bewoners verschillende bekende of gedeeltelijk onbekende datums aanvang" (deel van de testpersonen daarvan gebruikt)
    Gegeven met datum aanvang adreshouding 20230210 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000024 | Pietersen             | Bas               |              20210328 |
    En met datum aanvang adreshouding 20230316 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000012 | Pietersen             | Bas               |              20190917 |
      |                   000000036 | Jansen                | Bas               |              20210328 |
      |                   000000048 | Pietersen             | Annet             |              20210328 |
      |                   000000073 | Pietersen             | Bas               |              19980119 |
    En met datum aanvang adreshouding 20230000 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Pietersen             | Bas               |              20190917 |
      |                   000000103 | Jansen                | Bas               |              20190917 |
      |                   000000115 | Pietersen             | Annet             |              20190917 |
      |                   000000127 | Pietersen             | Bas               |              19980119 |
    En met datum aanvang adreshouding 20230300 zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000139 | Pietersen             | Bas               |              20190917 |
    # Datum vertrek is gedeeltelijk onbekend bij personen met volledig bekende datum aanvang
    En vervolgens zijn de volgende personen met datum aanvang adreshouding 20230300 ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000024 | Pietersen             | Bas               |              20210328 |
      |                   000000012 | Pietersen             | Bas               |              20190917 |
      |                   000000036 | Jansen                | Bas               |              20210328 |
      |                   000000048 | Pietersen             | Annet             |              20210328 |
      |                   000000073 | Pietersen             | Bas               |              19980119 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         |         2023-03-20 |
      | datumTot                         |         2023-04-01 |
      | adresseerbaarObjectIdentificatie |   0800010000000001 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-03-20 tot 2023-04-01 |
      | adresseerbaarObjectIdentificatie |          0800010000000001 |
    En heeft de bewoning mogelijke bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000103 | Bas            | Jansen             |       20190917 |
      |           000000115 | Annet          | Pietersen          |       20190917 |
      |           000000127 | Bas            | Pietersen          |       19980119 |
      |           000000097 | Bas            | Pietersen          |       20190917 |
      |           000000024 | Bas            | Pietersen          |       20210328 |
      |           000000139 | Bas            | Pietersen          |       20190917 |
      |           000000036 | Bas            | Jansen             |       20210328 |
      |           000000048 | Annet          | Pietersen          |       20210328 |
      |           000000073 | Bas            | Pietersen          |       19980119 |
      |           000000012 | Bas            | Pietersen          |       20190917 |

  @valideer-volgorde
  Abstract Scenario: Sorteren bewoners met gedeeltelijk onbekende datum aanvang en bekende aanvang vorige verblijfplaats
    # datum aanvang vorige verblijfplaats ligt in de onzekerheidsperiode van datum aanvang gevraagde verblijfplaats
    # bewoning rekent dan met datum aanvang vorige verblijfplaats + 1
    # maar sortering houdt hier geen rekening mee
    Gegeven met datum aanvang adreshouding <datum aanvang vorige Cees> zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Nielson               | Aart              |              20020411 |
    En vervolgens zijn de volgende personen met datum aanvang adreshouding <datum aanvang Cees> ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Nielson               | Aart              |              20020411 |
    En met datum aanvang adreshouding <datum aanvang Gerda> zijn de volgende personen ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000103 | Nielson               | Gerda             |              20020411 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         |         2024-01-01 |
      | datumTot                         |         2025-01-01 |
      | adresseerbaarObjectIdentificatie |   0800010000000002 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2024-01-01 tot 2025-01-01 |
      | adresseerbaarObjectIdentificatie |          0800010000000002 |
    En heeft de bewoning bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000097 | Aart           | Nielson            |       20020411 |
      |           000000103 | Gerda          | Nielson            |       20020411 |

    Voorbeelden:
      | datum aanvang vorige Cees | datum aanvang Cees | datum aanvang Gerda |
      |                  20230612 |           20230000 |            20230603 |
      |                  20230612 |           20230000 |            20230612 |
      |                  20230612 |           20230600 |            20230612 |

  @valideer-volgorde
  Abstract Scenario: Sorteren mogelijke bewoners met gedeeltelijk onbekende datum aanvang en bekende aanvang vorige verblijfplaats
    # datum aanvang vorige verblijfplaats ligt in de onzekerheidsperiode van datum aanvang gevraagde verblijfplaats
    # bewoning rekent dan met datum aanvang vorige verblijfplaats + 1
    # maar sortering houdt hier geen rekening mee
    Gegeven met datum aanvang adreshouding <datum aanvang vorige Cees> zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Nielson               | Aart              |              20020411 |
    En vervolgens zijn de volgende personen met datum aanvang adreshouding <datum aanvang Cees> ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000097 | Nielson               | Aart              |              20020411 |
    Gegeven met datum aanvang adreshouding <datum aanvang vorige Gerda> zijn de volgende personen ingeschreven op adres 'A1'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000103 | Nielson               | Gerda             |              20020411 |
    En vervolgens zijn de volgende personen met datum aanvang adreshouding <datum aanvang Gerda> ingeschreven op adres 'A2'
      | burgerservicenummer (01.20) | geslachtsnaam (02.40) | voornamen (02.10) | geboortedatum (03.10) |
      |                   000000103 | Nielson               | Gerda             |              20020411 |
    Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         |         2023-07-15 |
      | datumTot                         |         2023-08-01 |
      | adresseerbaarObjectIdentificatie |   0800010000000002 |
    Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2023-07-15 tot 2023-08-01 |
      | adresseerbaarObjectIdentificatie |          0800010000000002 |
    En heeft de bewoning mogelijke bewoners met de volgende gegevens
      | burgerservicenummer | naam.voornamen | naam.geslachtsnaam | geboorte.datum |
      |           000000097 | Aart           | Nielson            |       20020411 |
      |           000000103 | Gerda          | Nielson            |       20020411 |

    Voorbeelden:
      | datum aanvang vorige Cees | datum aanvang Cees | datum aanvang vorige Gerda | datum aanvang Gerda |
      |                  20230712 |           20230000 |                   20230711 |            20230000 |
      |                  20230712 |           20230000 |                   20230700 |            20230000 |
      |                  20230712 |           20230700 |                   20230711 |            20230700 |
