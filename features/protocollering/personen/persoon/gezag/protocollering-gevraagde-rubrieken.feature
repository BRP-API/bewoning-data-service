#language: nl

@protocollering
Functionaliteit: protocollering van de gevraagde gegevens voor gezag

  Regel: Met fields gevraagde velden worden geprotocolleerd als de elementnummers volgens Logisch ontwerp BRP

    Abstract Scenario: Met fields vragen om <fields> wordt vastgelegd als gevraagde rubrieken PAGZ01
      Gegeven de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
      | pl_id |
      | 1001  |
      En de response van de downstream api heeft de volgende headers
      | x-geleverde-pls |
      | 1001            |
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | 000000012                       |
      | fields              | <fields>                        |
      Dan heeft de persoon met burgerservicenummer '000000012' de volgende 'protocollering' gegevens
      | request_gevraagde_rubrieken |
      | PAGZ01                      |

      Voorbeelden:
      | fields                                     |
      | gezag                                      |
      | gezag.type                                 |
      | gezag.minderjarige                         |
      | gezag.ouders                               |
      | gezag.ouder                                |
      | gezag.derde                                |
      | gezag.derden                               |
      | gezag.minderjarige.burgerservicenummer     |
      | gezag.ouders.burgerservicenummer           |
      | gezag.ouder.burgerservicenummer            |
      | gezag.derde.burgerservicenummer            |
      | gezag.derden.burgerservicenummer           |
      | gezag.ouders,gezag.ouder                   |

    Abstract Scenario: Gezag vragen levert <testsitatie> en wordt geprotocolleerd als gevraagde rubrieken 'PAGZ01'
      Gegeven de persoon met burgerservicenummer '<burgerservicenummer>' heeft de volgende gegevens
      | pl_id |
      | 1001  |
      En de response van de downstream api heeft de volgende headers
      | x-geleverde-pls |
      | 1001            |
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | <burgerservicenummer>           |
      | fields              | gezag                           |
      Dan heeft de persoon met burgerservicenummer '<burgerservicenummer>' de volgende 'protocollering' gegevens
      | request_gevraagde_rubrieken |
      | PAGZ01                      |

      Voorbeelden:
      | burgerservicenummer | testsitatie               |
      | 000000012           | EenhoofdigOuderlijkGezag  |
      | 000000024           | TweehoofdigOuderlijkGezag |
      | 000000036           | GezamenlijkGezag          |
      | 000000048           | meerdere gezagsrelaties   |
      | 000000073           | geen gezagsrelatie        |
      | 000000085           | GezagNietTeBepalen        |
      | 000000097           | TijdelijkGeenGezag        |
