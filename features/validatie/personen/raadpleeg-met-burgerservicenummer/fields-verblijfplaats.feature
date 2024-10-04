#language: nl

@input-validatie
Functionaliteit: verblijfplaats velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat verblijfplaats veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                                            |
    | verblijfplaats                                                    |
    | verblijfplaats.type                                               |
    | verblijfplaats.datumIngangGeldigheid                              |
    | verblijfplaats.datumIngangGeldigheid.langFormaat                  |
    | verblijfplaats.datumIngangGeldigheid.type                         |
    | verblijfplaats.datumIngangGeldigheid.datum                        |
    | verblijfplaats.datumIngangGeldigheid.onbekend                     |
    | verblijfplaats.datumIngangGeldigheid.jaar                         |
    | verblijfplaats.datumIngangGeldigheid.maand                        |
    | verblijfplaats.datumVan                                           |
    | verblijfplaats.datumVan.langFormaat                               |
    | verblijfplaats.datumVan.type                                      |
    | verblijfplaats.datumVan.datum                                     |
    | verblijfplaats.datumVan.onbekend                                  |
    | verblijfplaats.datumVan.jaar                                      |
    | verblijfplaats.datumVan.maand                                     |
    | verblijfplaats.verblijfadres                                      |
    | verblijfplaats.verblijfadres.land                                 |
    | verblijfplaats.verblijfadres.land.code                            |
    | verblijfplaats.verblijfadres.land.omschrijving                    |
    | verblijfplaats.verblijfadres.regel1                               |
    | verblijfplaats.verblijfadres.regel2                               |
    | verblijfplaats.verblijfadres.regel3                               |
    | verblijfplaats.adresseerbaarObjectIdentificatie                   |
    | verblijfplaats.functieAdres                                       |
    | verblijfplaats.functieAdres.code                                  |
    | verblijfplaats.functieAdres.omschrijving                          |
    | verblijfplaats.nummeraanduidingIdentificatie                      |
    | verblijfplaats.verblijfadres.aanduidingBijHuisnummer              |
    | verblijfplaats.verblijfadres.aanduidingBijHuisnummer.code         |
    | verblijfplaats.verblijfadres.aanduidingBijHuisnummer.omschrijving |
    | verblijfplaats.verblijfadres.huisletter                           |
    | verblijfplaats.verblijfadres.huisnummer                           |
    | verblijfplaats.verblijfadres.huisnummertoevoeging                 |
    | verblijfplaats.verblijfadres.korteStraatnaam                      |
    | verblijfplaats.verblijfadres.officieleStraatnaam                  |
    | verblijfplaats.verblijfadres.postcode                             |
    | verblijfplaats.verblijfadres.woonplaats                           |
    | verblijfplaats.verblijfadres.locatiebeschrijving                  |
