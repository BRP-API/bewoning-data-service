#language: nl

@input-validatie
Functionaliteit: geboorte velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat geboorte veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                       |
    | geboorte                     |
    | geboorte.datum               |
    | geboorte.datum.langFormaat   |
    | geboorte.datum.type          |
    | geboorte.datum.datum         |
    | geboorte.datum.onbekend      |
    | geboorte.datum.jaar          |
    | geboorte.datum.maand         |
    | geboorte.land                |
    | geboorte.land.code           |
    | geboorte.land.omschrijving   |
    | geboorte.plaats              |
    | geboorte.plaats.code         |
    | geboorte.plaats.omschrijving |
