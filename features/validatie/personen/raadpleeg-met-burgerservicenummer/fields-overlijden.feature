#language: nl

@input-validatie
Functionaliteit: overlijden velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat overlijden veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                         |
    | overlijden                     |
    | overlijden.datum               |
    | overlijden.datum.langFormaat   |
    | overlijden.datum.type          |
    | overlijden.datum.datum         |
    | overlijden.datum.onbekend      |
    | overlijden.datum.jaar          |
    | overlijden.datum.maand         |
    | overlijden.land                |
    | overlijden.land.code           |
    | overlijden.land.omschrijving   |
    | overlijden.plaats              |
    | overlijden.plaats.code         |
    | overlijden.plaats.omschrijving |
