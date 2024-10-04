#language: nl

@input-validatie
Functionaliteit: naam velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat naam veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                    |
    | naam                                      |
    | naam.adellijkeTitelPredicaat              |
    | naam.adellijkeTitelPredicaat.code         |
    | naam.adellijkeTitelPredicaat.omschrijving |
    | naam.adellijkeTitelPredicaat.soort        |
    | naam.geslachtsnaam                        |
    | naam.volledigeNaam                        |
    | naam.voorletters                          |
    | naam.voornamen                            |
    | naam.voorvoegsel                          |
    | naam.aanduidingNaamgebruik                |
    | naam.aanduidingNaamgebruik.code           |
    | naam.aanduidingNaamgebruik.omschrijving   |
