#language: nl

@input-validatie
Functionaliteit: uitsluiting kiesrecht velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat uitsluiting kiesrecht veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                       |
    | uitsluitingKiesrecht                         |
    | uitsluitingKiesrecht.einddatum               |
    | uitsluitingKiesrecht.einddatum.langFormaat   |
    | uitsluitingKiesrecht.einddatum.type          |
    | uitsluitingKiesrecht.einddatum.datum         |
    | uitsluitingKiesrecht.einddatum.onbekend      |
    | uitsluitingKiesrecht.einddatum.jaar          |
    | uitsluitingKiesrecht.einddatum.maand         |
    | uitsluitingKiesrecht.uitgeslotenVanKiesrecht |
