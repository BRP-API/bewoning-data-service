#language: nl

@input-validatie
Functionaliteit: leeftijd veld vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat leeftijd veld
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields   |
    | leeftijd |
