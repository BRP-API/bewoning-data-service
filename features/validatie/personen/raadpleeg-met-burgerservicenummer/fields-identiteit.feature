#language: nl

@input-validatie
Functionaliteit: identiteit velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat identiteit veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields              |
    | aNummer             |
    | burgerservicenummer |
