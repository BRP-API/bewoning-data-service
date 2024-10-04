#language: nl

@input-validatie
Functionaliteit: indicatie curatele register veld vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat indicatie curatele register veld: <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                    |
    | indicatieCurateleRegister |
