#language: nl

@input-validatie
Functionaliteit: gemeente van inschrijving velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat gemeente van inschrijving veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                               |
    | gemeenteVanInschrijving              |
    | gemeenteVanInschrijving.code         |
    | gemeenteVanInschrijving.omschrijving |
