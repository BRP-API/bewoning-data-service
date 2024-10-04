#language: nl

@input-validatie
Functionaliteit: europees kiesrecht velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat europees kiesrecht veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                             |
    | europeesKiesrecht                                  |
    | europeesKiesrecht.aanduiding                       |
    | europeesKiesrecht.aanduiding.code                  |
    | europeesKiesrecht.aanduiding.omschrijving          |
    | europeesKiesrecht.einddatumUitsluiting             |
    | europeesKiesrecht.einddatumUitsluiting.langFormaat |
    | europeesKiesrecht.einddatumUitsluiting.type        |
    | europeesKiesrecht.einddatumUitsluiting.datum       |
    | europeesKiesrecht.einddatumUitsluiting.onbekend    |
    | europeesKiesrecht.einddatumUitsluiting.jaar        |
    | europeesKiesrecht.einddatumUitsluiting.maand       |
