#language: nl

@api
Functionaliteit: persoon met 'indicatie vastgesteld verblijft niet op adres' bij bewoning met periode

  Als afnemer
  Wil ik personen waarvan is vastgesteld dat zij niet meer op het adres verblijven niet worden geleverd als bewoner
  Zodat ik ze niet zelf hoeft uit te sluiten als bewoner

  Een burger kan bij de gemeente melden dat iemand anders ten onrechte op diens adres staat ingeschreven. 
  De gemeente doet daar dan onderzoek naar en kan concluderen dat deze andere persoon inderdaad niet meer op dat adres verblijft. 
  Wanneer tijdens de uitvoer van het onderzoek vastgesteld wordt dat een persoon niet meer woont op het adres waarop hij is ingeschreven in de BRP, 
  kan dit deel van het onderzoeksresultaat al worden opgenomen op de persoonslijst van de persoon. 
  Dit wordt gedaan door het zetten van aanduiding onderzoek 089999.
  Hiermee wordt geregistreerd dat is vastgesteld dat een persoon niet (langer) op het adres verblijft waarop hij ingeschreven staat, 
  maar dat het onderzoek naar het (nieuwe) woonadres nog loopt.
  Hierdoor kunnen eventuele problemen voor de nieuwe of oude medebewoners voorkomen worden.

  Wanneer onderzoek is afgesloten moet normaal gesproken worden aangenomen dat de resultaten van het onderzoek in de registratie zijn verwerkt.
  Het feit dat er een beëindigd onderzoek is met aanduiding vastgesteld geen bewoner meer (089999) kan dan worden genegeerd.
  
  Het kan echter voorkomen dat dit onderzoek wordt gesloten zonder dat het onderzoek inhoudelijk is afgerond. 
  Dit kan bijvoorbeeld gebeuren nadat de betreffende persoon zich inschrijft in een andere gemeente. 
  Deze andere gemeente zal of kan het onderzoek naar verblijf voor inschrijving in die gemeente vaak niet onderzoeken.
  Het onderzoek wordt dan in de registratie gesloten zonder dat is vastgesteld dat de registratie van verblijfplaatsen correct is.
  De aanduiding vastgesteld geen bewoner meer is dan nog steeds van belang, ook al is het onderzoek beëindigd.

  Wanneer onderzoek is beëindigd voor de datum aanvang van een volgend verblijf kunnen we zeker aannemen dat het onderzoek ook inhoudelijk afgerond is.
  Wanneer onderzoek is beëindigd op of na de datum aanvang van een volgend verblijf kunnen we niet met zekerheid zeggen of het onderzoek inhoudelijk afgerond is dan wel alleen administratie afgerond.
  In dat geval leveren we de persoon als mogelijke bewoner.
  

    Achtergrond:
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0800                 | 0800010000000001                         |
      En adres 'A2' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0800                 | 0800010000000002                         |    

  Regel: een persoon met aanduiding in onderzoek waarde '089999' of '589999' wordt niet geleverd als bewoner vanaf de ingangsdatum van het onderzoek

    Abstract Scenario: persoon verblijft niet meer op het gevraagde adres en <scenario>
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | aanduiding in onderzoek (83.10) | datum ingang onderzoek (83.20) | datum aanvang adreshouding (10.30) |
      | 0800                              | 089999                          | 20220526                       | 20200818                           |
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         | <datum van>        |
      | datumTot                         | <datum tot>        |
      | adresseerbaarObjectIdentificatie | 0800010000000001   |
      Dan heeft de response 0 bewoningen

      Voorbeelden:
      | datum van  | datum tot  | scenario                                  |
      | 2022-05-26 | 2023-05-26 | periode begint op de dag ingang onderzoek |
      | 2022-07-12 | 2022-08-12 | periode begint na de dag ingang onderzoek |

    Scenario: persoon verblijft niet meer op het gevraagde adres en is inmiddels ingeschreven op een ander adres en periode ligt na aanvang onderzoek
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | aanduiding in onderzoek (83.10) | datum ingang onderzoek (83.20) | datum aanvang adreshouding (10.30) |
      | 0800                              | 589999                          | 20220526                       | 20200818                           |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
      | 0800                              | 20220810                           |
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         | 2022-06-03         |
      | datumTot                         | 2022-07-01         |
      | adresseerbaarObjectIdentificatie | 0800010000000001   |
      Dan heeft de response 0 bewoningen


  Regel: een persoon met aanduiding in onderzoek waarde '089999' of '589999' wordt geleverd als bewoner tot de ingangsdatum van het onderzoek

    Scenario: persoon verblijft nog op het gevraagde adres en <scenario>
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | aanduiding in onderzoek (83.10) | datum ingang onderzoek (83.20) | datum aanvang adreshouding (10.30) |
      | 0800                              | 089999                          | 20220526                       | 20200818                           |
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         | <datum van>        |
      | datumTot                         | <datum tot>        |
      | adresseerbaarObjectIdentificatie | 0800010000000001   |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde           |
      | periode                          | <periode>        |
      | adresseerbaarObjectIdentificatie | 0800010000000001 |
      En heeft de bewoning een bewoner met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |
      En heeft de bewoner de volgende 'verblijfplaatsInOnderzoek' gegevens
      | aanduidingGegevensInOnderzoek | datumIngangOnderzoek |
      | 089999                        | 20220526             |

      Voorbeelden:
      | datum van  | datum tot  | periode                   | scenario                                       |
      | 2021-01-01 | 2022-01-01 | 2021-01-01 tot 2022-01-01 | periode ligt voor de datum ingang onderzoek    |
      | 2022-04-26 | 2022-05-26 | 2022-04-26 tot 2022-05-26 | periode loopt tot de datum ingang onderzoek    |
      | 2022-01-01 | 2023-01-01 | 2022-01-01 tot 2022-05-26 | periode loopt tot na de datum ingang onderzoek |

    Scenario: persoon verblijft niet meer op het gevraagde adres en is inmiddels ingeschreven op een ander adres en <scenario>
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | aanduiding in onderzoek (83.10) | datum ingang onderzoek (83.20) | datum aanvang adreshouding (10.30) |
      | 0800                              | 589999                          | 20220526                       | 20200818                           |
      En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
      | datum aanvang adreshouding (10.30) |
      | 20220810                           |
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         | <datum van>        |
      | datumTot                         | <datum tot>        |
      | adresseerbaarObjectIdentificatie | 0800010000000001   |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde           |
      | periode                          | <periode>        |
      | adresseerbaarObjectIdentificatie | 0800010000000001 |
      En heeft de bewoning een bewoner met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |
      En heeft de bewoner de volgende 'verblijfplaatsInOnderzoek' gegevens
      | aanduidingGegevensInOnderzoek | datumIngangOnderzoek |
      | 589999                        | 20220526             |

      Voorbeelden:
      | datum van  | datum tot  | periode                   | scenario                                            |
      | 2021-01-01 | 2022-01-01 | 2021-01-01 tot 2022-01-01 | periode ligt voor de datum aanvang van het verblijf |
      | 2022-04-26 | 2022-05-26 | 2022-04-26 tot 2022-05-26 | periode loopt tot de datum ingang onderzoek         |
      | 2022-01-01 | 2023-01-01 | 2022-01-01 tot 2022-05-26 | periode loopt tot na de datum ingang onderzoek      |


  Regel: een persoon met beëindigd onderzoek met aanduiding in onderzoek waarde '089999' op de actuele verblijfplaats wordt geleverd als bewoner

    Abstract Scenario: persoon heeft beëindigd onderzoek met aanduiding in onderzoek waarde '089999' en periode overlapt de duur van het onderzoek
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | aanduiding in onderzoek (83.10) | datum ingang onderzoek (83.20) | datum einde onderzoek (83.30) | datum aanvang adreshouding (10.30) |
      | 0800                              | 089999                          | 20220526                       | 20220810                      | 20200818                           |
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         | <datum van>        |
      | datumTot                         | <datum tot>        |
      | adresseerbaarObjectIdentificatie | 0800010000000001   |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde           |
      | periode                          | <periode>        |
      | adresseerbaarObjectIdentificatie | 0800010000000001 |
      En heeft de bewoning een bewoner met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |

      Voorbeelden:
      | datum van  | datum tot  | periode                   | scenario                                        |
      | 2022-01-01 | 2023-01-01 | 2022-01-01 tot 2023-01-01 | periode overlapt de hele duur van het onderzoek |
      | 2022-01-01 | 2022-05-01 | 2022-01-01 tot 2022-05-01 | periode loopt tot voor ingang onderzoek         |
      | 2022-06-01 | 2022-07-01 | 2022-06-01 tot 2022-07-01 | periode valt binnen onderzoek                   |


  Regel: een persoon met aanduiding in onderzoek waarde '089999' of '589999' op een historische verblijfplaats en het onderzoek is beëindigd voor datum aanvang van de volgende verblijfplaats wordt geleverd als bewoner

    Abstract Scenario: persoon heeft beëindigd onderzoek met aanduiding in onderzoek waarde '<aanduiding onderzoek>' en is inmiddels ingeschreven op een ander adres en onderzoek is beëindigd voor aanvang van de volgende verblijfplaats en <scenario>
      Gegeven de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | aanduiding in onderzoek (83.10) | datum ingang onderzoek (83.20) | datum einde onderzoek (83.30) | datum aanvang adreshouding (10.30) |
      | 0800                              | <aanduiding onderzoek>          | 20220526                       | 20220810                      | 20200818                           |
      En de persoon is vervolgens ingeschreven op een buitenlands adres met de volgende gegevens
      | land (13.10) | datum aanvang adres buitenland (13.20) |
      | 0000         | 20220901                               |
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         | <datum van>        |
      | datumTot                         | <datum tot>        |
      | adresseerbaarObjectIdentificatie | 0800010000000001   |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                      |
      | periode                          | <datum van> tot <datum tot> |
      | adresseerbaarObjectIdentificatie | 0800010000000001            |
      En heeft de bewoning een bewoner met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |

      Voorbeelden:
      | aanduiding onderzoek | datum van  | datum tot  | scenario                                        |
      | 089999               | 2022-01-01 | 2022-09-01 | periode overlapt de hele duur van het onderzoek |
      | 089999               | 2022-01-01 | 2022-05-01 | periode loopt tot voor ingang onderzoek         |
      | 089999               | 2022-06-01 | 2022-08-01 | periode valt binnen onderzoek                   |
      | 089999               | 2022-08-12 | 2022-08-17 | periode valt na beëindigen van onderzoek        |
      | 589999               | 2022-01-01 | 2022-09-01 | periode overlapt de hele duur van het onderzoek |
      | 589999               | 2022-01-01 | 2022-05-01 | periode loopt tot voor ingang onderzoek         |
      | 589999               | 2022-06-01 | 2022-08-01 | periode valt binnen onderzoek                   |
      | 589999               | 2022-08-12 | 2022-08-29 | periode valt na beëindigen van onderzoek        |


  Regel: een persoon met aanduiding in onderzoek waarde '089999' of '589999' op een historische verblijfplaats en het onderzoek is beëindigd op of na datum aanvang van de volgende verblijfplaats wordt geleverd als bewoner tot de ingangsdatum van het onderzoek en wordt geleverd als mogelijke bewoner vanaf de ingangsdatum van het onderzoek
    # wanneer het onderzoek is beëindigd op of na datum aanvang van de volgende verblijfplaats is niet met zekerheid te bepalen waarom het onderzoek beëindigd is
    # het onderzoek kan bijvoorbeeld beëindigd zijn door een andere gemeente dan waar het onderzoek betrekking op had, zonder dat die gemeente heeft bepaald dat 'vastgesteld geen bewoner meer' niet meer van toepassing is

    Abstract Scenario: persoon heeft beëindigd onderzoek met aanduiding in onderzoek waarde '<aanduiding onderzoek>' en is inmiddels ingeschreven op een ander adres en <scenario>
      Gegeven adres 'A1' heeft de volgende gegevens
      | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
      | 0800                 | 0800010000000001                         |
      En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
      | gemeente van inschrijving (09.10) | aanduiding in onderzoek (83.10) | datum ingang onderzoek (83.20) | datum einde onderzoek (83.30) | datum aanvang adreshouding (10.30) |
      | 0800                              | <aanduiding onderzoek>          | 20220526                       | <datum einde onderzoek>       | 20200818                           |
      En de persoon is vervolgens ingeschreven op een buitenlands adres met de volgende gegevens
      | land (13.10) | datum aanvang adres buitenland (13.20) |
      | 0000         | 20220901                               |
      Als bewoningen wordt gezocht met de volgende parameters
      | naam                             | waarde             |
      | type                             | BewoningMetPeriode |
      | datumVan                         | 2022-01-01         |
      | datumTot                         | 2023-01-01         |
      | adresseerbaarObjectIdentificatie | 0800010000000001   |
      Dan heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2022-01-01 tot 2022-05-26 |
      | adresseerbaarObjectIdentificatie | 0800010000000001          |
      En heeft de bewoning een bewoner met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |
      En heeft de response een bewoning met de volgende gegevens
      | naam                             | waarde                    |
      | periode                          | 2022-05-26 tot 2022-09-01 |
      | adresseerbaarObjectIdentificatie | 0800010000000001          |
      En heeft de bewoning een mogelijke bewoner met de volgende gegevens
      | burgerservicenummer |
      | 000000024           |

      Voorbeelden:
      | aanduiding onderzoek | datum einde onderzoek | scenario                                                                  |
      | 089999               | 20220902              | onderzoek is beëindigd na de datum aanvang van de volgende verblijfplaats |
      | 589999               | 20220902              | onderzoek is beëindigd na de datum aanvang van de volgende verblijfplaats |
      | 089999               | 20220901              | onderzoek is beëindigd op de datum aanvang van de volgende verblijfplaats |
      | 589999               | 20220901              | onderzoek is beëindigd op de datum aanvang van de volgende verblijfplaats |
