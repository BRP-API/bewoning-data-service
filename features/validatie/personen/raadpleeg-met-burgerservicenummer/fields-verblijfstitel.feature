#language: nl

@input-validatie
Functionaliteit: verblijfstitel velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat verblijfstitel veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                 |
    | verblijfstitel                         |
    | verblijfstitel.aanduiding              |
    | verblijfstitel.aanduiding.code         |
    | verblijfstitel.aanduiding.omschrijving |
    | verblijfstitel.datumEinde              |
    | verblijfstitel.datumEinde.langFormaat  |
    | verblijfstitel.datumEinde.type         |
    | verblijfstitel.datumEinde.datum        |
    | verblijfstitel.datumEinde.onbekend     |
    | verblijfstitel.datumEinde.jaar         |
    | verblijfstitel.datumEinde.maand        |
    | verblijfstitel.datumIngang             |
    | verblijfstitel.datumIngang.langFormaat |
    | verblijfstitel.datumIngang.type        |
    | verblijfstitel.datumIngang.datum       |
    | verblijfstitel.datumIngang.onbekend    |
    | verblijfstitel.datumIngang.jaar        |
    | verblijfstitel.datumIngang.maand       |
