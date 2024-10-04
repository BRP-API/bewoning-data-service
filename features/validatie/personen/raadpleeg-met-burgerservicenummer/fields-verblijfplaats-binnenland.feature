#language: nl

@input-validatie
Functionaliteit: verblijfplaats binnenland velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat verblijfplaats binnenland veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                                                      |
    | verblijfplaatsBinnenland                                                    |
    | verblijfplaatsBinnenland.type                                               |
    | verblijfplaatsBinnenland.datumIngangGeldigheid                              |
    | verblijfplaatsBinnenland.datumIngangGeldigheid.langFormaat                  |
    | verblijfplaatsBinnenland.datumIngangGeldigheid.type                         |
    | verblijfplaatsBinnenland.datumIngangGeldigheid.datum                        |
    | verblijfplaatsBinnenland.datumIngangGeldigheid.onbekend                     |
    | verblijfplaatsBinnenland.datumIngangGeldigheid.jaar                         |
    | verblijfplaatsBinnenland.datumIngangGeldigheid.maand                        |
    | verblijfplaatsBinnenland.datumVan                                           |
    | verblijfplaatsBinnenland.datumVan.langFormaat                               |
    | verblijfplaatsBinnenland.datumVan.type                                      |
    | verblijfplaatsBinnenland.datumVan.datum                                     |
    | verblijfplaatsBinnenland.datumVan.onbekend                                  |
    | verblijfplaatsBinnenland.datumVan.jaar                                      |
    | verblijfplaatsBinnenland.datumVan.maand                                     |
    | verblijfplaatsBinnenland.verblijfadres                                      |
    | verblijfplaatsBinnenland.adresseerbaarObjectIdentificatie                   |
    | verblijfplaatsBinnenland.functieAdres                                       |
    | verblijfplaatsBinnenland.functieAdres.code                                  |
    | verblijfplaatsBinnenland.functieAdres.omschrijving                          |
    | verblijfplaatsBinnenland.nummeraanduidingIdentificatie                      |
    | verblijfplaatsBinnenland.verblijfadres.aanduidingBijHuisnummer              |
    | verblijfplaatsBinnenland.verblijfadres.aanduidingBijHuisnummer.code         |
    | verblijfplaatsBinnenland.verblijfadres.aanduidingBijHuisnummer.omschrijving |
    | verblijfplaatsBinnenland.verblijfadres.huisletter                           |
    | verblijfplaatsBinnenland.verblijfadres.huisnummer                           |
    | verblijfplaatsBinnenland.verblijfadres.huisnummertoevoeging                 |
    | verblijfplaatsBinnenland.verblijfadres.korteStraatnaam                      |
    | verblijfplaatsBinnenland.verblijfadres.officieleStraatnaam                  |
    | verblijfplaatsBinnenland.verblijfadres.postcode                             |
    | verblijfplaatsBinnenland.verblijfadres.woonplaats                           |
    | verblijfplaatsBinnenland.verblijfadres.locatiebeschrijving                  |
